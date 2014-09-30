/*
Copyright (c) 2011+, HL7, Inc
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, 
are permitted provided that the following conditions are met:

 * Redistributions of source code must retain the above copyright notice, this 
   list of conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above copyright notice, 
   this list of conditions and the following disclaimer in the documentation 
   and/or other materials provided with the distribution.
 * Neither the name of HL7 nor the names of its contributors may be used to 
   endorse or promote products derived from this software without specific 
   prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
POSSIBILITY OF SUCH DAMAGE.

*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Publication
{
    public class HierarchicalTableGenerator
    {

        public class Piece
        {
            private String tag;
            private String reference;
            private String text;
            private String hint;
            private String style;

            public Piece(String tag)
            {
                this.tag = tag;
            }

            public Piece(String reference, String text, String hint)
            {
                this.reference = reference;
                this.text = text;
                this.hint = hint;
            }
            public String getReference()
            {
                return reference;
            }
            public void setReference(String value)
            {
                reference = value;
            }
            public String getText()
            {
                return text;
            }
            public String getHint()
            {
                return hint;
            }

            public String getTag()
            {
                return tag;
            }

            public String getStyle()
            {
                return style;
            }

            public Piece setStyle(String style)
            {
                this.style = style;
                return this;
            }

            public Piece addStyle(String style)
            {
                if (this.style != null)
                    this.style = this.style + ": " + style;
                else
                    this.style = style;
                return this;
            }

        }

        public class Cell
        {
            internal List<Piece> pieces = new List<HierarchicalTableGenerator.Piece>();

            public Cell()
            {
            }

            public Cell(String prefix, String reference, String text, String hint, String suffix)
            {

                if (!String.IsNullOrEmpty(prefix)) pieces.Add(new Piece(null, prefix, null));
                pieces.Add(new Piece(reference, text, hint));
                if (!String.IsNullOrEmpty(suffix)) pieces.Add(new Piece(null, suffix, null));
            }

            public List<Piece> getPieces()
            {
                return pieces;
            }
            public Cell addPiece(Piece piece)
            {
                pieces.Add(piece);
                return this;
            }
        }

        public class Title : Cell
        {
            internal int width;

            public Title(String prefix, String reference, String text, String hint, String suffix, int width)
                : base(prefix, reference, text, hint, suffix)
            {

                this.width = width;
            }
        }

        public class Row
        {
            private List<Row> subRows = new List<HierarchicalTableGenerator.Row>();
            private List<Cell> cells = new List<HierarchicalTableGenerator.Cell>();
            private String icon;
            private String anchor;

            public List<Row> getSubRows()
            {
                return subRows;
            }
            public List<Cell> getCells()
            {
                return cells;
            }
            public String getIcon()
            {
                return icon;
            }
            public void setIcon(String icon)
            {
                this.icon = icon;
            }
            public String getAnchor()
            {
                return anchor;
            }
            public void setAnchor(String anchor)
            {
                this.anchor = anchor;
            }


        }

        public class TableModel
        {
            private List<Title> titles = new List<HierarchicalTableGenerator.Title>();
            private List<Row> rows = new List<HierarchicalTableGenerator.Row>();
            public List<Title> getTitles()
            {
                return titles;
            }
            public List<Row> getRows()
            {
                return rows;
            }
        }


        private String dest;

        /**
         * There are circumstances where the table has to present in the absence of a stable supporting infrastructure.
         * and the file paths cannot be guaranteed. For these reasons, you can tell the builder to inline all the graphics
         * (all the styles are inlined anyway, since the table fbuiler has even less control over the styling
         *  
         */
        private bool inLineGraphics;


        public HierarchicalTableGenerator(String dest, bool inlineGraphics)
        {
            this.dest = dest;
            this.inLineGraphics = inlineGraphics;
        }

        public TableModel initNormalTable()
        {
            TableModel model = new TableModel();

            model.getTitles().Add(new Title(null, null, "Name", null, null, 0));
            model.getTitles().Add(new Title(null, null, "Card.", null, null, 0));
            model.getTitles().Add(new Title(null, null, "Type", null, null, 100));
            model.getTitles().Add(new Title(null, null, "Description & Constraints", null, null, 0));
            return model;
        }


        public XNode generate(TableModel model)
        {
            checkModel(model);
            var table = new XElement(XmlNs.XHTMLNS + "table",
                    new XAttribute("border", "0"),
                    new XAttribute("cellspacing", "0"),
                    new XAttribute("cellpadding", "0"),
                    new XAttribute("style", "border: 0px; font-size: 11px; font-family: verdana; vertical-align: top;"));

            var tr = new XElement(XmlNs.XHTML + "tr",
                new XAttribute("style", "border: 1px #F0F0F0 solid; font-size: 11px; font-family: verdana; vertical-align: top;"));

            table.Add(tr);

            foreach (Title t in model.getTitles())
            {
                var tc = renderCell(tr, t, "th", null, null, false, null);
                if (t.width != 0)
                    tc.Add(new XAttribute("style", "width: " + t.width.ToString() + "px"));
            }

            foreach (Row r in model.getRows())
            {
                renderRow(table, r, 0, new List<bool>());
            }

            return table;
        }


        private void renderRow(XElement table, Row r, int indent, List<Boolean> indents)
        {
            var tr = new XElement(XmlNs.XHTMLNS + "tr",
                new XAttribute("style", "border: 0px; padding:0px; vertical-align: top; background-color: white;"));
            table.Add(tr);

            bool first = true;
            foreach (Cell t in r.getCells())
            {
                renderCell(tr, t, "td", first ? r.getIcon() : null, first ? indents : null, r.getSubRows().Any(), first ? r.getAnchor() : null);
                first = false;
            }

            // table.addText("\r\n");

            for (int i = 0; i < r.getSubRows().Count; i++)
            {
                Row c = r.getSubRows()[i];
                var ind = new List<Boolean>();
                ind.AddRange(indents);
                if (i == r.getSubRows().Count - 1)
                    ind.Add(true);
                else
                    ind.Add(false);
                renderRow(table, c, indent + 1, ind);
            }
        }


        private XElement renderCell(XElement tr, Cell c, String name, String icon, List<Boolean> indents, bool hasChildren, String anchor)
        {
            var tc = new XElement(XmlNs.XHTMLNS + name, new XAttribute("class", "heirarchy"));
            tr.Add(tc);

            if (indents != null)
            {
                var spacerImg = new XElement(XmlNs.XHTMLNS + "img",
                   new XAttribute("src", srcFor("tbl_spacer.png")),
                   new XAttribute("class", "heirarchy"),
                   new XAttribute("alt", "."));
                tc.Add(spacerImg);

                tc.Add(new XAttribute("style", "vertical-align: top; text-align : left; padding:0px 4px 0px 4px; white-space: nowrap; background-image: url("
                          + checkExists(indents, hasChildren) + ")"));

                for (int i = 0; i < indents.Count - 1; i++)
                {
                    if (indents[i])
                    {
                        var blankImg = new XElement(XmlNs.XHTMLNS + "img",
                            new XAttribute("src", srcFor("tbl_blank.png")),
                            new XAttribute("class", "heirarchy"),
                            new XAttribute("alt", "."));
                        tc.Add(blankImg);
                    }
                    else
                    {
                        var vlineImage = new XElement(XmlNs.XHTMLNS + "img",
                            new XAttribute("src", srcFor("tbl_vline.png")),
                            new XAttribute("class", "heirarchy"),
                            new XAttribute("alt", "."));
                    }
                }

                if (indents.Any())
                {
                    if (indents[indents.Count - 1])
                    {
                        var vjoinEndImage = new XElement(XmlNs.XHTMLNS + "img",
                            new XAttribute("src", srcFor("tbl_vjoin_end.png")),
                            new XAttribute("class", "heirarchy"),
                            new XAttribute("alt", "."));
                        tc.Add(vjoinEndImage);
                    }
                    else
                    {
                        var vjoinImage = new XElement(XmlNs.XHTMLNS + "img",
                            new XAttribute("src", srcFor("tbl_vjoin.png")),
                            new XAttribute("class", "heirarchy"),
                            new XAttribute("alt", "."));
                        tc.Add(vjoinImage);
                    }
                }
            }
            else
                tc.Add(new XAttribute("style", "vertical-align: top; text-align : left; padding:0px 4px 0px 4px"));

            if (!String.IsNullOrEmpty(icon))
            {
                tc.Add(new XElement(XmlNs.XHTMLNS + "img",
                    new XAttribute("src", srcFor(icon)),
                    new XAttribute("class", "heirarchy"),
                    new XAttribute("style", "background-color: white;"),
                    new XAttribute("alt", ".")));
                //tc.addText(" ");
            }

            foreach (Piece p in c.pieces)
            {
                if (!String.IsNullOrEmpty(p.getTag()))
                {
                    var newTag = new XElement(XmlNs.XHTMLNS + p.getTag());
                    tc.Add(newTag);
                    addStyle(newTag, p);
                }
                else if (!String.IsNullOrEmpty(p.getReference()))
                {
                    var newTag = new XElement(XmlNs.XHTMLNS + "a");
                    tc.Add(newTag);
                    XElement a = addStyle(newTag, p);
                    a.Add(new XAttribute("href", p.getReference()));

                    if (!String.IsNullOrEmpty(p.getHint()))
                        a.Add(new XAttribute("title", p.getHint()));

                    a.Add(new XText(p.getText()));
                }
                else
                {
                    if (!String.IsNullOrEmpty(p.getHint()))
                    {
                        var span = new XElement(XmlNs.XHTMLNS + "span");
                        tc.Add(span);

                        XElement s = addStyle(span, p);
                        s.Add(new XAttribute("title", p.getHint()));
                        s.Add(new XText(p.getText()));
                    }
                    else if (p.getStyle() != null)
                    {
                        var span = new XElement(XmlNs.XHTMLNS + "span");
                        tc.Add(span);
                        XElement s = addStyle(span, p);
                        s.Add(new XText(p.getText()));
                    }
                    else
                        tc.Add(new XText(p.getText()));
                }
            }
            if (!String.IsNullOrEmpty(anchor))
            {
                var a = new XElement(XmlNs.XHTMLNS + "a",
                            new XAttribute("name", nmTokenize(anchor)));
                // .addText(" ")
                tc.Add(a);
            }

            return tc;
        }


        private XElement addStyle(XElement node, Piece p)
        {
            if (p.getStyle() != null)
                node.Add(new XAttribute("style", p.getStyle()));
            return node;
        }

        private String nmTokenize(String anchor)
        {
            return anchor.Replace("[", "_").Replace("]", "_");
        }


        private String srcFor(String filename)
        {
            if (inLineGraphics)
            {
                StringBuilder b = new StringBuilder();
                b.Append("data: image/png;base64,");
                byte[] bytes = File.ReadAllBytes(Path.Combine(dest, filename));

                b.Append(Convert.ToBase64String(bytes));
                return b.ToString();
            }
            else
                return filename;
        }


        private void checkModel(TableModel model)
        {
            check(model.getRows().Any(), "Must have rows");
            check(model.getTitles().Any(), "Must have titles");
            foreach (Cell c in model.getTitles())
                check(c);
            foreach (Row r in model.getRows())
                check(r, "rows", model.getTitles().Count);
        }


        private void check(Cell c)
        {
            bool hasText = false;
            foreach (Piece p in c.pieces)
                if (!String.IsNullOrEmpty(p.getText()))
                    hasText = true;
            check(hasText, "Title cells must have text");
        }


        private void check(Row r, String str, int size)
        {
            check(r.getCells().Count == size, "All rows must have the same number of columns as the titles");
            foreach (Row c in r.getSubRows())
                check(c, "rows", size);
        }


        private String checkExists(List<Boolean> indents, bool hasChildren) 
        {
          StringBuilder b = new StringBuilder();
         
            if (inLineGraphics) 
            {
                MemoryStream bytes = new MemoryStream();
                genImage(indents, hasChildren, bytes);
                b.Append("data: image/png;base64,");
                var encodeBase64 = Convert.ToBase64String(bytes.ToArray());
                b.Append(encodeBase64);
            } 
            else 
            {
                b.Append("tbl_bck");
                foreach (Boolean i in indents)
                    b.Append(i ? "0" : "1");

                if (hasChildren)
                    b.Append("1");
                else
                    b.Append("0");

                b.Append(".png");
            
                String file = Path.Combine(dest, b.ToString());

                if (!File.Exists(file))
                {
                    var stream = new FileStream(file, FileMode.Create);
                    genImage(indents, hasChildren, stream);
                }
          }

          return b.ToString();
        }


        private void genImage(List<Boolean> indents, bool hasChildren, Stream stream)
        {
            var bi = new Bitmap(400,2, PixelFormat.Canonical);
            var graphics = Graphics.FromImage(bi);

            graphics.DrawRectangle(new Pen(Color.White), 0, 0, 400, 2);

            for (int i = 0; i < indents.Count; i++) 
            {
                if (!indents[i])
                    bi.SetPixel(12+(i*16), 0, Color.Black);
            }

            if (hasChildren)
            {
                bi.SetPixel(12 + (indents.Count * 16), 0, Color.Black);
            }

            bi.Save(stream, ImageFormat.Png);
        }


        private void check(bool check, String message)
        {
            if (!check)
                throw new Exception(message);
        }
    }
}
