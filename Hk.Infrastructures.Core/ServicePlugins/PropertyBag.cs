using System;
using System.Collections.Generic;

namespace Hk.Infrastructures.Core.ServicePlugins
{
    public class PropertyBag
    {
        #region Fields

        // A Hashtable to contain the properties in the bag
        private Dictionary<string, object> _objPropertyCollection = new Dictionary<string, object>();

        #endregion

        #region Instance Indexers

        /// <summary>
        /// Indexer which retrieves a property from the PropertyBag based on 
        /// the property name
        /// </summary>
        public object this[string name]
        {
            get
            {
                if (_objPropertyCollection == null)
                {
                    _objPropertyCollection = new Dictionary<string, object>();
                }

                // An instance of the Property that will be returned
                object objProperty = null;

                // If the PropertyBag already contains a property whose name matches
                // the property required, ...
                if (_objPropertyCollection.ContainsKey(name))
                {
                    // ... then return the pre-existing property
                    objProperty = _objPropertyCollection[name];
                }
                return objProperty;
            }
            set
            {
                if (!_objPropertyCollection.ContainsKey(name))
                {
                    _objPropertyCollection.Add(name, value);
                }
            }
        }

        #endregion

        #region Instance Properties

        #endregion
    }
}
