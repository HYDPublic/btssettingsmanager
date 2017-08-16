using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EnterpriseSingleSignOn.Interop;

namespace BTSSettingsManager
{
    /// <summary>
    /// Provides an object with a property bag in which the object can save its properties persistently.
    /// </summary>
    public class SSOPropertyBag : IPropertyBag
    {
        /// <summary>
        /// Properties saved in the property bag.
        /// </summary>
        private IDictionary<string, string> Properties;

        /// <summary>
        /// Initializes a instance of the SSOPropertyPag object.
        /// </summary>
        public SSOPropertyBag()
        {
            this.Properties = new Dictionary<string, string>();
        }

        /// <summary>
        /// Determine if the named property exists in the property bag.
        /// </summary>
        /// <param name="propName">The address of the name of the property to read. This cannot be NULL.</param>
        /// <returns>True, if the named property exists. False otherwise.</returns>
        private bool Contains(string propName)
        {
            return this.Properties.ContainsKey(propName);
        }

        /// <summary>
        /// Tells the property bag to read the named property into a caller-initialized VARIANT.
        /// </summary>
        /// <param name="propName">The address of the name of the property to read. This cannot be NULL.</param>
        /// <param name="ptrVar">The address of the caller-initialized VARIANT that receives the property value on output. The function must set the type field and the value field in the VARIANT before it returns. If the caller initialized the pVar->vt field on entry, the property bag attempts to change its corresponding value to this type. If the caller sets pVar->vt to VT_EMPTY, the property bag can use whatever type is convenient.</param>
        /// <param name="errorLog">The address of the caller's error log in which the property bag stores any errors that occur during reads. This can be NULL; in which case, the caller does not receive errors.</param>
        public void Read(string propName, out object ptrVar, int errorLog)
        {
            if (this.Contains(propName))
            {
                ptrVar = this.Properties[propName];
            }
            else
            {
                ptrVar = null;
            }
        }

        /// <summary>
        /// Tells the property bag to save the named property in a caller-initialized VARIANT.
        /// </summary>
        /// <param name="propName">The address of a string containing the name of the property to write. This cannot be NULL.</param>
        /// <param name="ptrVar">The address of the caller-initialized VARIANT that holds the property value to save. The caller owns this VARIANT, and is responsible for all of its allocations. That is, the property bag does not attempt to free data in the VARIANT.</param>
        public void Write(string propName, ref object ptrVar)
        {
            if (this.Contains(propName))
            {
                this.Properties[propName] = (string)ptrVar;
            }
            else
            {
                this.Properties.Add(propName, (string)ptrVar);
            }
        }
    }
}
