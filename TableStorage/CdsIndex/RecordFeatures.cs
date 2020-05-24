using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.CosmosDB.Table;

namespace TableStorage.Model
{
    class RecordFeatures : TableEntity
    {
        public RecordFeatures()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerEntity"/> class.
        /// Defines the PK and RK.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <param name="firstName">The first name.</param>
        public RecordFeatures(string entityName, string entityId)
        {
            PartitionKey = entityName;
            RowKey = entityId;

            Embedding = new float[128];
            for (int i = 0; i <Embedding.Length; i++)
            {
                Embedding[i] = 0;
            }

            Embedding[0] = float.Parse(entityId);
        }

        /// <summary>
        /// Gets or sets the email address for the customer.
        /// A property for use in Table storage must be a public property of a 
        /// supported type that exposes both a getter and a setter.        
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public float[] Embedding { get; set; }
    }
}
