using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Publication
{

    public class ProfileTableGenerator
    { //extends TableGenerator {

        protected bool InlineGraphics { get; set; }
        protected string OutputPath { get; set; }


        public ProfileTableGenerator(String outputPath, String pageName, bool inlineGraphics)
        {
            OutputPath = outputPath;
            InlineGraphics = inlineGraphics;
            // super(dest, page, pageName, inlineGraphics);
        }


        protected bool dictLinks()
        {
            return false;
        }


        public XElement generate(Profile p, bool extensionsOnly)
        {
            var gen = new HierarchicalTableGenerator(OutputPath, InlineGraphics);
            TableModel model = gen.initNormalTable();

            genProfile(model.getRows(), p, extensionsOnly);

            return gen.generate(model);
        }

        private void genProfile(List<Row> rows, Profile profile, bool extensionsOnly)
        {
            if (!extensionsOnly)
            {
                var r = new  Row();
                rows.Add(r);
                r.setIcon("icon_profile.png");
                r.getCells().Add(new  Cell(null, null, profile.Name, null, null));
                r.getCells().Add(new  Cell());
                r.getCells().Add(new  Cell(null, null, "Profile", null, null));
                r.getCells().Add(new  Cell(null, null, profile.Description, null, null));

                foreach (var s in profile.Structure)
                {
                    var re = new  Row();
                    r.getSubRows().Add(re);
                    re.setIcon("icon_resource.png");
                    re.getCells().Add(new  Cell(null, null, s.Name, null, null));
                    re.getCells().Add(new  Cell(null, null, "", null, null));
                    re.getCells().Add(new  Cell(null, null, s.Type, null, null));

                    re.getCells().Add(new  Cell(null, null, s.Element[0].Definition.Short, null, null));     // DSTU1
                    //re.getCells().Add(new  Cell(null, null, s.Base, null, null));       // DSTU2
                }
            }

            if (profile.ExtensionDefn.Any() || extensionsOnly)
            {
                var re = new  Row();
                rows.Add(re);
                re.setIcon("icon_profile.png");
                re.getCells().Add(new  Cell(null, null, "Extensions", null, null));
                re.getCells().Add(new  Cell());
                re.getCells().Add(new  Cell());

                re.getCells().Add(new  Cell(null, null, "Extensions defined by this profile", null, null)); // DSTU1
                //re.getCells().Add(new  Cell(null, null, "Extensions defined by the URL \"" + profile.Url + "\"", null, null)); // DSTU2

                foreach (var ext in profile.ExtensionDefn)
                {
                    genExtension(re.getSubRows(), ext, true);
                }
            }

        }


        private void genExtension(List<Row> rows, Profile.ProfileExtensionDefnComponent ext, bool root)
        {
            var r = new  Row();
            rows.Add(r);
            r.setAnchor(ext.Code);

            r.setIcon("icon_extension_simple.png");     // DSTU1
            //if (ext.getChildren().isEmpty())    // DSTU2
            //  r.setIcon("icon_extension_simple.png");
            //else
            //r.setIcon("icon_extension_complex.png");

            r.getCells().Add(new  Cell(null, "test.html", ext.Code, null, null));
            r.getCells().Add(new  Cell(null, null, ext.Definition.DescribeCardinality(), null, null));   //TODO: create rendering extension
            r.getCells().Add(new  Cell(null, null, ext.Definition.DescribeTypeCode(), null, null));       //TODO: create rendering extension
            //    r.getCells().add(gen.new Cell(null, null, ext.getDefinition().getShortDefn(), null, null));
            //    if (root)
            //      r.getCells().add(gen.new Cell(null, null, describeExtensionContext(ext), null, null));
            //    else
            //      r.getCells().add(gen.new Cell());
            if (root)
            {
                r.getCells().Add(new  Cell(null, null, ext.Definition.Short, null, null)
                      .addPiece(new  Piece("br"))
                      .addPiece(new  Piece(null, describeExtensionContext(ext), null)));
            }
            else
            {
                r.getCells().Add(new  Cell(null, null, ext.Definition.Short, null, null));
            }

            //foreach (var child in ext.Children)    // DSTU2
            //{
            //  genExtension(gen, r.getSubRows(), child, false);
            //}
        }

        private String describeExtensionContext(Profile.ProfileExtensionDefnComponent ext)
        {
            switch (ext.ContextType)
            {
                case Profile.ExtensionContext.Datatype:
                    return "Use on data type: " + ext.DescribeContext();
                case Profile.ExtensionContext.Extension:
                    return "Use on extension: " + ext.DescribeContext();
                case Profile.ExtensionContext.Resource:
                    return "Use on resource: " + ext.DescribeContext();
                case Profile.ExtensionContext.Mapping:
                    return "Use where element has mapping: " + ext.DescribeContext();
            }

            return null;
        }
    }
}


#if ORIGINAL_JAVA_CODE
package org.hl7.fhir.definitions.generators.specification;

import java.util.List;

import org.hl7.fhir.definitions.model.BindingSpecification;
import org.hl7.fhir.definitions.model.Definitions;
import org.hl7.fhir.definitions.model.ElementDefn;
import org.hl7.fhir.definitions.model.ExtensionDefn;
import org.hl7.fhir.definitions.model.Invariant;
import org.hl7.fhir.definitions.model.ProfileDefn;
import org.hl7.fhir.definitions.model.ResourceDefn;
import org.hl7.fhir.definitions.model.TypeRef;
import org.hl7.fhir.definitions.model.BindingSpecification.Binding;
import org.hl7.fhir.definitions.model.BindingSpecification.BindingStrength;
import org.hl7.fhir.instance.model.Profile;
import org.hl7.fhir.instance.model.Profile.ProfileStructureComponent;
import org.hl7.fhir.tools.publisher.PageProcessor;
import org.hl7.fhir.utilities.Utilities;
import org.hl7.fhir.utilities.xhtml.HeirarchicalTableGenerator.Cell;
import org.hl7.fhir.utilities.xhtml.HeirarchicalTableGenerator.Piece;
import org.hl7.fhir.utilities.xhtml.HeirarchicalTableGenerator.Row;
import org.hl7.fhir.utilities.xhtml.HeirarchicalTableGenerator.TableModel;
import org.hl7.fhir.utilities.xhtml.HeirarchicalTableGenerator;
import org.hl7.fhir.utilities.xhtml.XhtmlNode;
import org.hl7.fhir.utilities.xhtml.genImage;

public class ProfileTableGenerator extends TableGenerator {
  public ProfileTableGenerator(String dest, PageProcessor page, String pageName, boolean inlineGraphics) throws Exception {
    super(dest, page, pageName, inlineGraphics);
  }

  
  @Override
  protected boolean dictLinks() {
    return false;
  }


  public XhtmlNode generate(ProfileDefn p, String root, boolean extensionsOnly) throws Exception {
    HeirarchicalTableGenerator gen = new HeirarchicalTableGenerator(dest, inlineGraphics);
    TableModel model = gen.initNormalTable();
    
    genProfile(gen, model.getRows(), p, root, extensionsOnly);
    
    return gen.generate(model);
  }

  private void genProfile(HeirarchicalTableGenerator gen, List<Row> rows, ProfileDefn p, String root, boolean extensionsOnly) throws Exception {
    Profile profile = p.getSource();
    
    if (!extensionsOnly) {
      Row r = gen.new Row();
      rows.add(r);
      r.setIcon("icon_profile.png");
      r.getCells().add(gen.new Cell(null, null, profile.getNameSimple(), null, null));
      r.getCells().add(gen.new Cell());
      r.getCells().add(gen.new Cell(null, null, "Profile", null, null));
      r.getCells().add(gen.new Cell(null, null, profile.getDescriptionSimple(), null, null));

      for (ProfileStructureComponent s : profile.getStructure()) {
        Row re = gen.new Row();
        r.getSubRows().add(re);
        re.setIcon("icon_resource.png");
        re.getCells().add(gen.new Cell(null, null, s.getNameSimple(), null, null));
        re.getCells().add(gen.new Cell(null, null, "", null, null));
        re.getCells().add(gen.new Cell(null, null, s.getTypeSimple(), null, null));
        re.getCells().add(gen.new Cell(null, null, s.getBaseSimple(), null, null));
      }
    }
    
    if (!p.getExtensions().isEmpty() || extensionsOnly) {
      Row re = gen.new Row();
      rows.add(re);
      re.setIcon("icon_profile.png");
      re.getCells().add(gen.new Cell(null, null, "Extensions", null, null));
      re.getCells().add(gen.new Cell());
      re.getCells().add(gen.new Cell());
      re.getCells().add(gen.new Cell(null, null, "Extensions defined by the URL \""+profile.getUrlSimple()+"\"", null, null));

      for (ExtensionDefn ext : p.getExtensions()) {
        genExtension(gen, re.getSubRows(), ext, true);
      }
    }
    
  }

  private void genResourceProfile(HeirarchicalTableGenerator gen, List<Row> rows, ResourceDefn res) throws Exception {
    Row r = gen.new Row();
    rows.add(r);
    r.setIcon("icon_resource.png");
    r.getCells().add(gen.new Cell(null, null, res.getRoot().getProfileName(), null, null));
    r.getCells().add(gen.new Cell());
    r.getCells().add(gen.new Cell(null, null, res.getRoot().getName(), null, null));
    r.getCells().add(gen.new Cell(null, null, res.getDefinition(), null, null));
//    r.getCells().add(gen.new Cell());
    
    for (ElementDefn c : res.getRoot().getElements())
      r.getSubRows().add(genElement(c, gen, false, res.getRoot().getProfileName(), true));
  }

  private void genExtension(HeirarchicalTableGenerator gen, List<Row> rows, ExtensionDefn ext, boolean root) {
    Row r = gen.new Row();
    rows.add(r);
    r.setAnchor(ext.getCode());
    if (ext.getChildren().isEmpty())
      r.setIcon("icon_extension_simple.png");
    else
      r.setIcon("icon_extension_complex.png");
    r.getCells().add(gen.new Cell(null, null, ext.getCode(), null, null));
    r.getCells().add(gen.new Cell(null, null, ext.getDefinition().describeCardinality(), null, null));
    r.getCells().add(gen.new Cell(null, null, ext.getDefinition().typeCode(), null, null));
//    r.getCells().add(gen.new Cell(null, null, ext.getDefinition().getShortDefn(), null, null));
//    if (root)
//      r.getCells().add(gen.new Cell(null, null, describeExtensionContext(ext), null, null));
//    else
//      r.getCells().add(gen.new Cell());
    if (root)
      r.getCells().add(gen.new Cell(null, null, ext.getDefinition().getShortDefn(), null, null).addPiece(gen.new Piece("br")).addPiece(gen.new Piece(null, describeExtensionContext(ext), null)));
    else
      r.getCells().add(gen.new Cell(null, null, ext.getDefinition().getShortDefn(), null, null));
    for (ExtensionDefn child : ext.getChildren()) {
      genExtension(gen, r.getSubRows(), child, false);
    }
  }

  private String describeExtensionContext(ExtensionDefn ext) {
    switch (ext.getType()) {
    case DataType: return "Use on data type: "+ext.getContext();
    case Elements: return "Use on element: "+ext.getContext();
    case Extension: return "Use on extension: "+ext.getContext();
    case Resource: return "Use on resource: "+ext.getContext();
    case Mapping: return "Use where element has mapping: "+ext.getContext();
    }
    return null;
  }
}
#endif