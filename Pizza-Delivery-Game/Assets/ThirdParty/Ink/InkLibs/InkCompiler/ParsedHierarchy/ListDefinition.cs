using System;
using System.Collections.Generic;
using Ink.Runtime;

namespace Ink.Parsed
{
    public class ListDefinition : Object
    {
        public Identifier identifier;
        public List<ListElementDefinition> itemDefinitions;

        public VariableAssignment variableAssignment;

        public Runtime.ListDefinition runtimeListDefinition {
            get {
                var allItems = new Dictionary<string, int> ();
                foreach (var e in itemDefinitions) {
                    if( !allItems.ContainsKey(e.name) )
                        allItems.Add (e.name, e.seriesValue);
                    else
                        Error("List '"+identifier+"' contains dupicate items called '"+e.name+"'");
                }

                return new Runtime.ListDefinition (identifier?.name, allItems);
            }
        }

        public ListElementDefinition ItemNamed (string itemName)
        {
            if (_elementsByName == null) {
                _elementsByName = new Dictionary<string, ListElementDefinition> ();
                foreach (var el in itemDefinitions) {
                    _elementsByName [el.name] = el;
                }
            }

            ListElementDefinition foundElement;
            if (_elementsByName.TryGetValue (itemName, out foundElement))
                return foundElement;

            return null;
        }

        public ListDefinition (List<ListElementDefinition> elements)
        {
            itemDefinitions = elements;

            int currentValue = 1;
            foreach (var e in itemDefinitions) {
                if (e.explicitValue != null)
                    currentValue = e.explicitValue.Value;

                e.seriesValue = currentValue;

                currentValue++;
            }

            AddContent (elements);
        }

        public override Runtime.Object GenerateRuntimeObject ()
        {
            var initialValues = new InkList ();
            foreach (var itemDef in itemDefinitions) {
                if (itemDef.inInitialList) {
                    var item = new InkListItem (identifier?.name, itemDef.name);
                    initialValues [item] = itemDef.seriesValue;
                }
            }

            // Set origin name, so
            initialValues.SetInitialOriginName (identifier?.name);

            return new ListValue (initialValues);
        }

        public override void ResolveReferences (Story context)
        {
            base.ResolveReferences (context);

            context.CheckForNamingCollisions (this, identifier, Story.SymbolType.List);
        }

        public override string typeName {
            get {
                return "List definition";
            }
        }

        Dictionary<string, ListElementDefinition> _elementsByName;
    }

    public class ListElementDefinition : Object
    {
        public string name
        {
            get { return identifier?.name; }
        }
        public Identifier identifier;
        public int? explicitValue;
        public int seriesValue;
        public bool inInitialList;

        public string fullName {
            get {
                var parentList = parent as ListDefinition;
                if (parentList == null)
                    throw new Exception ("Can't get full name without a parent list");

                return parentList.identifier + "." + name;
            }
        }

        public ListElementDefinition (Identifier identifier, bool inInitialList, int? explicitValue = null)
        {
            this.identifier = identifier;
            this.inInitialList = inInitialList;
            this.explicitValue = explicitValue;
        }

        public override Runtime.Object GenerateRuntimeObject ()
        {
            throw new NotImplementedException ();
        }

        public override void ResolveReferences (Story context)
        {
            base.ResolveReferences (context);

            context.CheckForNamingCollisions (this, identifier, Story.SymbolType.ListItem);
        }

        public override string typeName {
        	get {
                return "List element";
            }
        }
    }
}
