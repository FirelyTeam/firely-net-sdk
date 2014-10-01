using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;

namespace Hl7.Fhir.Publication
{
    public class StructureGenerator
    {
        private class UnusedTracker 
        {
		    public bool used;
	    }

        public XElement generateStructureTable(String defFile, Profile.ProfileStructureComponent structure, bool diff, String imageFolder, 
                    bool inlineGraphics, Profile profile, String profileBaseFileName) 
        {
            HierarchicalTableGenerator gen = new HierarchicalTableGenerator(imageFolder, inlineGraphics);
            TableModel model = new TableModel();

            // List<Profile.ElementComponent> list = diff ? structure.getDifferential().getElement() : structure.getSnapshot().getElement();   DSTU2
            var list = structure.Element;
    
            genElement(defFile == null ? null : defFile+"#"+structure.Name+".", gen, model.getRows(), list[0], list, profile, diff, profileBaseFileName);
            return gen.generate(model);
        }


        private void genElement(String defPath, HierarchicalTableGenerator gen, List<Row> rows, Profile.ElementComponent element, List<Profile.ElementComponent> all, 
                    Profile profile, bool showMissing, String profileBaseFileName)
        {
            if(onlyInformationIsMapping(all, element)) return;  // we don't even show it in this case

            Row row = new Row();
            row.setAnchor(element.Path);
            String s = element.GetNameFromPath();

            bool hasDef = element.Definition != null;
            bool ext = false;
    
            if (s == "extension" || s == "modifierExtension")
            { 
                row.setIcon("icon_extension_simple.png");
                ext = true;
            }
            else if (!hasDef || element.Definition.Type.Count == 0)
            {
                row.setIcon("icon_element.gif");
            }
            else if (hasDef && element.Definition.Type.Count > 1)
            {
                if (allTypesAre(element.Definition.Type, "Reference"))
                    row.setIcon("icon_reference.png");
                else
                    row.setIcon("icon_choice.gif");
            }
            else if (hasDef && element.Definition.Type[0].Code.StartsWith("@"))
            {
                //TODO: That's not a legal code, will this ever appear?
                //I am pretty sure this depends on ElementDefn.NameReference
                row.setIcon("icon_reuse.png");
            }
            else if (hasDef && isPrimitive(element.Definition.Type[0].Code))
                row.setIcon("icon_primitive.png");
            else if (hasDef && isReference(element.Definition.Type[0].Code))
                row.setIcon("icon_reference.png");
            else if (hasDef && isDataType(element.Definition.Type[0].Code))
                row.setIcon("icon_datatype.gif");
            else
                row.setIcon("icon_resource.png");

            String reference = defPath == null ? null : defPath + makePathLink(element);
            UnusedTracker used = new UnusedTracker();
            used.used = true;
            
            Cell left = new Cell(null, reference, s, !hasDef ? null : element.Definition.Formal, null);
            row.getCells().Add(left);
    
            if (ext)
            {
                // If this element (row) in the table is an extension...
                if (element.Definition != null && element.Definition.Type.Count == 1 && element.Definition.Type[0].Profile != null) 
                {
                    Profile.ProfileExtensionDefnComponent extDefn = pkp.getExtensionDefinition(profile, element.Definition.Type[0].Profile);
        
                    if (extDefn == null) 
                    {
                        row.getCells().Add(new Cell(null, null, !hasDef ? null : describeCardinality(element.Definition, null, used), null, null));
                        row.getCells().Add(new Cell(null, null, "?? "+element.Definition.Type[0].Profile, null, null));
                        generateDescription(gen, row, element, null, used.used, profile.Url, element.Definition.Type[0].Profile, pkp, profile);
                    }
                    else 
                    {
                        row.getCells().Add(new Cell(null, null, !hasDef ? null : describeCardinality(element.Definition, extDefn.Definition, used), null, null));
                        genTypes(gen, pkp, row, extDefn.Definition, profileBaseFileName, profile);
                        generateDescription(gen, row, element, extDefn.Definition, used.used, profile.Url, element.Definition.Type[0].Profile, pkp, profile);
                    } 
                }
                else if (element.Definition != null) 
                {
                    row.getCells().Add(new Cell(null, null, !hasDef ? null : describeCardinality(element.Definition, null, used), null, null));
                    genTypes(gen, pkp, row, element, profileBaseFileName, profile);
                    generateDescription(gen, row, element, null, used.used, null, null, pkp, profile);
                } 
                else 
                {
                    row.getCells().Add(new Cell(null, null, !hasDef ? null : describeCardinality(element.Definition, null, used), null, null));
                    row.getCells().Add(new Cell());
                    generateDescription(gen, row, element, null, used.used, null, null, pkp, profile);
                }
            } 
            else 
            {
                row.getCells().Add(new Cell(null, null, !hasDef ? null : describeCardinality(element.Definition, null, used), null, null));
                
                if (hasDef)
                    genTypes(gen, pkp, row, element, profileBaseFileName, profile);
                else
                    row.getCells().Add(new Cell());
        
                generateDescription(gen, row, element, null, used.used, null, null, pkp, profile);
            }
      
            if (element.Slicing != null) 
            {
                row.setIcon("icon_slice.png");
                row.getCells()[2].getPieces().Clear();
        
                foreach (Cell cell in row.getCells())
                    foreach (Piece p in cell.getPieces())
                    {
                        p.addStyle("font-style: italic");
                    }        
            }

            if (used.used || showMissing)
                rows.Add(row);
      
            if (!used.used) 
            {
                foreach (Cell cell in row.getCells())
                foreach (Piece p in cell.getPieces()) 
                {
                    p.setStyle("text-decoration:line-through");
                    p.setReference(null);
                }
            } 
            else
            {
                List<Profile.ElementComponent> children = getChildren(all, element);
                foreach (Profile.ElementComponent child in children)
                    genElement(defPath, gen, row.getSubRows(), child, all, profile, pkp, showMissing, profileBaseFileName);
            }
        }
   
        private bool onlyInformationIsMapping(List<Profile.ElementComponent> all, Profile.ElementComponent element)
        {
            return false;
            //TODO: Port
        }

        private bool allTypesAre(List<Profile.TypeRefComponent> types, String name) 
        {
            return types.All( t => t.Code == name );
        }

        private String makePathLink(Profile.ElementComponent element)
        {
            if (element.Name == null)
                return element.Path;
            if (!element.Path.Contains("."))
                return element.Name;
            return element.Path.Substring(0, element.Path.LastIndexOf(".")) + "." + element.Name;
        }

        //TODO: Determine dynamically based on core profiles?
        private bool isDataType(String value)
        {
            return new[] { "Identifier", "HumanName", "Address", "ContactPoint", "Timing", "Quantity", "Attachment", "Range",
                  "Period", "Ratio", "CodeableConcept", "Coding", "SampledData", "Age", "Distance", "Duration", "Count", "Money" }.Contains(value);
        }

        private bool isReference(String value)
        {
            return value == "Reference";
        }

        private bool isPrimitive(String value)
        {
            return new[] { "boolean", "integer", "decimal", "base64Binary", "instant", "string", "date", "dateTime", "code", "oid", "uuid", "id" }.Contains(value);
        }

        private String describeCardinality(Profile.ElementDefinitionComponent definition, Profile.ElementDefinitionComponent fallback, UnusedTracker tracker)
        {
            var min = definition.Min;
            var max = definition.Max;

            if (min == null && fallback != null)
                min = fallback.Min;
            if (max == null && fallback != null)
                max = fallback.Max;

            tracker.used = max == null || !(max == "0");

            if (min == null && max == null)
                return null;
            else
                return (min == null ? "" : min.ToString() + ".." + (max == null ? "" : Max));
        }
    }
}


#if ORIGINAL_JAVA_CODE
public XhtmlNode generateTable(String defFile, ProfileStructureComponent structure, boolean diff, String imageFolder, boolean inlineGraphics, Profile profile, ProfileKnowledgeProvider pkp, String profileBaseFileName) throws Exception {
    HeirarchicalTableGenerator gen = new HeirarchicalTableGenerator(imageFolder, inlineGraphics);
    TableModel model = gen.initNormalTable();
    List<ElementComponent> list = diff ? structure.getDifferential().getElement() : structure.getSnapshot().getElement();
    genElement(defFile == null ? null : defFile+"#"+structure.getNameSimple()+".", gen, model.getRows(), list.get(0), list, profile, pkp, diff, profileBaseFileName);
    return gen.generate(model);
  }

  private void genElement(String defPath, HeirarchicalTableGenerator gen, List<Row> rows, ElementComponent element, List<ElementComponent> all, Profile profile, ProfileKnowledgeProvider pkp, boolean showMissing, String profileBaseFileName) throws Exception {
    if (!onlyInformationIsMapping(all, element)) { // we don't even show it in this case
    Row row = gen.new Row();
    row.setAnchor(element.getPathSimple());
    String s = tail(element.getPathSimple());
    boolean hasDef = element.Definition != null;
    boolean ext = false;
    if (s.equals("extension") || s.equals("modifierExtension")) { 
      row.setIcon("icon_extension_simple.png");
      ext = true;
    } else if (!hasDef || element.Definition.getType().size() == 0)
      row.setIcon("icon_element.gif");
    else if (hasDef && element.Definition.getType().size() > 1) {
      if (allTypesAre(element.Definition.getType(), "Reference"))
        row.setIcon("icon_reference.png");
      else
        row.setIcon("icon_choice.gif");
    } else if (hasDef && element.Definition.Type[0].getCode().getValue().startsWith("@"))
      row.setIcon("icon_reuse.png");
    else if (hasDef && isPrimitive(element.Definition.Type[0].getCode().getValue()))
      row.setIcon("icon_primitive.png");
    else if (hasDef && isReference(element.Definition.Type[0].getCode().getValue()))
      row.setIcon("icon_reference.png");
    else if (hasDef && isDataType(element.Definition.Type[0].getCode().getValue()))
      row.setIcon("icon_datatype.gif");
    else
      row.setIcon("icon_resource.png");
    String ref = defPath == null ? null : defPath + makePathLink(element);
    UnusedTracker used = new UnusedTracker();
    used.used = true;
    Cell left = gen.new Cell(null, ref, s, !hasDef ? null : element.Definition.getFormalSimple(), null);
    row.getCells().add(left);
    if (ext) {
      if (element.Definition != null && element.Definition.getType().size() == 1 && element.Definition.Type[0].getProfile() != null) {
        ExtensionDefinition extDefn = pkp.getExtensionDefinition(profile, element.Definition.Type[0].Profile);
        if (extDefn == null) {
            row.getCells().add(gen.new Cell(null, null, !hasDef ? null : describeCardinality(element.Definition, null, used), null, null));
            row.getCells().add(gen.new Cell(null, null, "?? "+element.Definition.Type[0].Profile, null, null));
            generateDescription(gen, row, element, null, used.used, profile.getUrlSimple(), element.Definition.Type[0].Profile, pkp, profile);
          } else {
            row.getCells().add(gen.new Cell(null, null, !hasDef ? null : describeCardinality(element.Definition, extDefn.getDefn().getElement().get(0).getDefinition(), used), null, null));
            genTypes(gen, pkp, row, extDefn.getDefn().getElement().get(0), profileBaseFileName, profile);
            generateDescription(gen, row, element, extDefn.getDefn().getElement().get(0), used.used, profile.getUrlSimple(), element.Definition.Type[0].Profile, pkp, profile);
        }
      } else if (element.Definition != null) {
          row.getCells().add(gen.new Cell(null, null, !hasDef ? null : describeCardinality(element.Definition, null, used), null, null));
          genTypes(gen, pkp, row, element, profileBaseFileName, profile);
          generateDescription(gen, row, element, null, used.used, null, null, pkp, profile);
      } else {
          row.getCells().add(gen.new Cell(null, null, !hasDef ? null : describeCardinality(element.Definition, null, used), null, null));
          row.getCells().add(gen.new Cell());
          generateDescription(gen, row, element, null, used.used, null, null, pkp, profile);
      }
    } else {
        row.getCells().add(gen.new Cell(null, null, !hasDef ? null : describeCardinality(element.Definition, null, used), null, null));
        if (hasDef)
          genTypes(gen, pkp, row, element, profileBaseFileName, profile);
        else
          row.getCells().add(gen.new Cell());
        generateDescription(gen, row, element, null, used.used, null, null, pkp, profile);
      }
      if (element.getSlicing() != null) {
        row.setIcon("icon_slice.png");
        row.getCells().get(2).getPieces().clear();
        for (Cell cell : row.getCells())
          for (Piece p : cell.getPieces()) {
            p.addStyle("font-style: italic");
          }
        
      }
      if (used.used || showMissing)
        rows.add(row);
      if (!used.used) {
        for (Cell cell : row.getCells())
          for (Piece p : cell.getPieces()) {
            p.setStyle("text-decoration:line-through");
            p.setReference(null);
          }
      } else{
        List<ElementComponent> children = getChildren(all, element);
        for (ElementComponent child : children)
          genElement(defPath, gen, row.getSubRows(), child, all, profile, pkp, showMissing, profileBaseFileName);
      }
    }
  }


  private String makePathLink(ElementComponent element) {
    if (element.getName() == null)
      return element.getPathSimple();
    if (!element.getPathSimple().contains("."))
      return element.getNameSimple();
    return element.getPathSimple().substring(0, element.getPathSimple().lastIndexOf("."))+"."+element.getNameSimple();
  }

  private Cell generateDescription(HeirarchicalTableGenerator gen, Row row, ElementComponent definition, ElementComponent fallback, boolean used, String baseURL, String url, ProfileKnowledgeProvider pkp, Profile profile) {
    // TODO Auto-generated method stub
    Cell c = gen.new Cell();
    row.getCells().add(c);                

    if (used) {
      if (definition.getDefinition() != null && definition.getDefinition().getShort() != null) {
        if (!c.getPieces().isEmpty()) c.addPiece(gen.new Piece("br"));
        c.addPiece(gen.new Piece(null, definition.getDefinition().getShortSimple(), null));
      } else if (fallback != null && fallback.getDefinition() != null && fallback.getDefinition().getShort() != null) {
        if (!c.getPieces().isEmpty()) c.addPiece(gen.new Piece("br"));
        c.addPiece(gen.new Piece(null, fallback.getDefinition().getShortSimple(), null));
      }
      if (url != null) {
        if (!c.getPieces().isEmpty()) c.addPiece(gen.new Piece("br"));
        String fullUrl = url.startsWith("#") ? baseURL+url : url;
        String ref = pkp.getLinkForExtension(profile, url);
        c.getPieces().add(gen.new Piece(null, "URL: ", null).addStyle("font-weight:bold"));
        c.getPieces().add(gen.new Piece(ref, fullUrl, null));
      }

      if (definition.getSlicing() != null) {
        if (!c.getPieces().isEmpty()) c.addPiece(gen.new Piece("br"));
        c.getPieces().add(gen.new Piece(null, "Slice: ", null).addStyle("font-weight:bold"));
        c.getPieces().add(gen.new Piece(null, describeSlice(definition.getSlicing()), null));
      }
      if (definition.getDefinition() != null) {
        if (definition.getDefinition().getBinding() != null) {
          if (!c.getPieces().isEmpty()) c.addPiece(gen.new Piece("br"));
          String ref = pkp.resolveBinding(definition.getDefinition().getBinding());
          c.getPieces().add(gen.new Piece(null, "Binding: ", null).addStyle("font-weight:bold"));
          c.getPieces().add(gen.new Piece(ref, definition.getDefinition().getBinding().getNameSimple(), null));
        }
        for (ElementDefinitionConstraintComponent inv : definition.getDefinition().getConstraint()) {
          if (!c.getPieces().isEmpty()) c.addPiece(gen.new Piece("br"));
          c.getPieces().add(gen.new Piece(null, "Inv-"+inv.getKeySimple()+": ", null).addStyle("font-weight:bold"));
          c.getPieces().add(gen.new Piece(null, inv.getHumanSimple(), null));
        }
        if (definition.getDefinition().getValue() != null) {        
          if (!c.getPieces().isEmpty()) c.addPiece(gen.new Piece("br"));
          c.getPieces().add(gen.new Piece(null, "Fixed Value: ", null).addStyle("font-weight:bold"));
          c.getPieces().add(gen.new Piece(null, "(todo)", null));
        }
        // ?? example from definition    
      }
    }
    return c;
  }

  public String describeSlice(ElementSlicingComponent slicing) {
    return (slicing.getOrderedSimple() ? "Ordered, " : "Unordered, ")+describe(slicing.getRulesSimple())+", by "+slicing.getDiscriminatorSimple();
  }

  private String describe(ResourceSlicingRules rules) {
    switch (rules) {
    case closed : return "Closed";
    case open : return "Open";
    case openAtEnd : return "Open At End";
    default:
      return "??";
    }
  }

  private boolean onlyInformationIsMapping(List<ElementComponent> list, ElementComponent e) {
    return (e.getName() == null && e.getSlicing() == null && (e.getDefinition() == null || onlyInformationIsMapping(e.getDefinition()))) &&
        getChildren(list, e).isEmpty();
  }

  private boolean onlyInformationIsMapping(ElementDefinitionComponent d) {
    return d.getShort() == null && d.getFormal() == null && 
        d.getRequirements() == null && d.getSynonym().isEmpty() && d.getMin() == null &&
        d.getMax() == null && d.getType().isEmpty() && d.getNameReference() == null && 
        d.getExample() == null && d.getValue() == null && d.getMaxLength() == null &&
        d.getCondition().isEmpty() && d.getConstraint().isEmpty() && d.getMustSupport() == null &&
        d.getBinding() == null;
  }

  private boolean allTypesAre(List<TypeRefComponent> types, String name) {
    for (TypeRefComponent t : types) {
      if (!t.getCodeSimple().equals(name))
        return false;
    }
    return true;
  }

  private List<ElementComponent> getChildren(List<ElementComponent> all, ElementComponent element) {
    List<ElementComponent> result = new ArrayList<Profile.ElementComponent>();
    int i = all.indexOf(element)+1;
    while (i < all.size() && all.get(i).getPathSimple().length() > element.getPathSimple().length()) {
      if ((all.get(i).getPathSimple().substring(0, element.getPathSimple().length()+1).equals(element.getPathSimple()+".")) && !all.get(i).getPathSimple().substring(element.getPathSimple().length()+1).contains(".")) 
        result.add(all.get(i));
      i++;
    }
    return result;
  }

  private String tail(String path) {
    if (path.contains("."))
      return path.substring(path.lastIndexOf('.')+1);
    else
      return path;
  }

  private boolean isDataType(String value) {
    return Utilities.existsInList(value, "Identifier", "HumanName", "Address", "ContactPoint", "Timing", "Quantity", "Attachment", "Range", 
          "Period", "Ratio", "CodeableConcept", "Coding", "SampledData", "Age", "Distance", "Duration", "Count", "Money");
  }

  private boolean isReference(String value) {
    return value.equals("Reference");
  }

  private boolean isPrimitive(String value) {
    return Utilities.existsInList(value, "boolean", "integer", "decimal", "base64Binary", "instant", "string", "date", "dateTime", "code", "oid", "uuid", "id");
  }

  public static String summarise(Profile p, ProfileKnowledgeProvider pkp) throws Exception {
    if (p.getExtensionDefn().isEmpty())
      return "This profile has constraints on the following resources: "+listStructures(p, pkp);
    else if (p.getStructure().isEmpty())
      return "This profile defines "+Integer.toString(p.getExtensionDefn().size())+" extensions.";
    else
      return "This profile defines "+Integer.toString(p.getExtensionDefn().size())+" extensions and has constraints on the following resources: "+listStructures(p, pkp);
  }

  private static String listStructures(Profile p, ProfileKnowledgeProvider pkp) throws Exception {
    StringBuilder b = new StringBuilder();
    boolean first = true;
    for (ProfileStructureComponent s : p.getStructure()) {
      if (first)
        first = false;
      else
        b.append(", ");
      if (pkp != null && pkp.hasLinkFor(s.getTypeSimple()))
        b.append("<a href=\""+pkp.getLinkFor(s.getTypeSimple())+"\">"+s.getTypeSimple()+"</a>");
      else
        b.append(s.getTypeSimple());
    }
    return b.toString();
  }


  public StrucResult getStructure(Profile source, String url) throws Exception {
    Profile profile;
    String code;
    if (url.startsWith("#")) {
      profile = source;
      code = url.substring(1);
    } else {
      String[] parts = url.split("\\#");
      if (!context.getProfiles().containsKey(parts[0])) {
      	if (parts[0].startsWith("http:") || parts[0].startsWith("https:")) {
        	String[] ps = parts[0].split("\\/Profile\\/");
        	if (ps.length != 2)
        		throw new Exception("Unable to understand address of profile: "+parts[0]);
        	FHIRClient client = new FHIRSimpleClient();
        	client.initialize(ps[0]);
        	AtomEntry<Profile> ae = client.read(Profile.class, ps[1]);
        	context.getProfiles().put(parts[0], ae);
      	} else
      		return null;
      }
      profile = context.getProfiles().get(parts[0]).getResource();
      code = parts.length < 2 ? null : parts[1];
    }

    if (profile != null) {
      ProfileStructureComponent structure = null;
      for (ProfileStructureComponent s : profile.getStructure()) {
        if (s.getNameSimple().equals(code) || s.getPublishSimple()) 
          structure = s;
      }
      if (structure != null)
        return new StrucResult(profile, structure);
    }
    return null;
  }

  public ExtensionResult getExtensionDefn(Profile source, String url) {
    Profile profile;
    String code;
    if (url.startsWith("#")) {
      profile = source;
      code = url.substring(1);
    } else {
      String[] parts = url.split("\\#");
      profile = context.getProfiles().get(parts[0]).getResource();
      code = parts[1];
    }

    if (profile != null) {
      ProfileExtensionDefnComponent defn = null;
      for (ProfileExtensionDefnComponent s : profile.getExtensionDefn()) {
        if (s.getCodeSimple().equals(code)) 
          defn = s;
      }
      if (defn != null)
        return new ExtensionResult(profile, defn);
    }
    return null;
  }


#endif
