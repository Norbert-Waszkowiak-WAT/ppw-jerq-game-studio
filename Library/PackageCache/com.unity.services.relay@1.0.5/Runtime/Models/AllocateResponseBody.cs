//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.Services.Relay.Http;



namespace Unity.Services.Relay.Models
{
    /// <summary>
    /// AllocateResponseBody model
    /// </summary>
    [Preserve]
    [DataContract(Name = "AllocateResponseBody")]
    public class AllocateResponseBody
    {
        /// <summary>
        /// Creates an instance of AllocateResponseBody.
        /// </summary>
        /// <param name="meta">meta param</param>
        /// <param name="data">data param</param>
        /// <param name="links">links param</param>
        [Preserve]
        public AllocateResponseBody(ResponseMeta meta, AllocationData data, ResponseLinks links = default)
        {
            Meta = meta;
            Links = links;
            Data = data;
        }

        /// <summary>
        /// Parameter meta of AllocateResponseBody
        /// </summary>
        [Preserve]
        [DataMember(Name = "meta", IsRequired = true, EmitDefaultValue = true)]
        public ResponseMeta Meta{ get; }
        
        /// <summary>
        /// Parameter links of AllocateResponseBody
        /// </summary>
        [Preserve]
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public ResponseLinks Links{ get; }
        
        /// <summary>
        /// Parameter data of AllocateResponseBody
        /// </summary>
        [Preserve]
        [DataMember(Name = "data", IsRequired = true, EmitDefaultValue = true)]
        public AllocationData Data{ get; }
    
        /// <summary>
        /// Formats a AllocateResponseBody into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        internal string SerializeAsPathParam()
        {
            var serializedModel = "";

            if (Meta != null)
            {
                serializedModel += "meta," + Meta.ToString() + ",";
            }
            if (Links != null)
            {
                serializedModel += "links," + Links.ToString() + ",";
            }
            if (Data != null)
            {
                serializedModel += "data," + Data.ToString();
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a AllocateResponseBody as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        internal Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();

            return dictionary;
        }
    }
}