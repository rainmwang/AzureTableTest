using Microsoft.Azure.CosmosDB.Table;
using Microsoft.Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using TableStorage.Model;
using Microsoft.Extensions.Logging;
using Microsoft.BizQA.Common;
using TableStorage.CdsIndex.Utility;

namespace TableStorage.CdsIndex
{
    class FeatureGenerator
    {
        private ILogger _logger;
        private IPerfCounterCollector _perfCounterCollector;
        public FeatureGenerator(IPerfCounterCollector perfCounterCollector)
        {
            _logger = new MyDummyLogger();
            _perfCounterCollector = perfCounterCollector;
        }

        public async Task BatchInsertRecordFeaturesAsync()
        {
            try
            {
                string tableName = "testorg" + DateTime.Now.ToString("yyyyMMddhhmm");

                // Create or reference an existing table
                CloudTable table = await Common.CreateTableAsync(tableName);

                var entities = new string[] { "Account", "Contact", "Lead", "Opportunity", "Case"};
                var recordCount = 5000;
                var batchSize = 100;

                foreach (var entity in entities)
                {
                    _logger.LogInformation($"Start processing entity {entity}");

                    var curRecordNum = 0;
                    while (curRecordNum < recordCount)
                    {
                        using (var perfScope = _perfCounterCollector.BeginPerfScope("FeatureGeneration/BatchInsert"))
                        {
                            var recordsToInsert = Math.Min(batchSize, recordCount - curRecordNum);
                            await InsertOneBatchAsync(table, entity, curRecordNum, recordsToInsert);

                            curRecordNum += recordsToInsert;
                        }
                    }

                    _logger.LogInformation($"Complete processing entity {entity}");
                }
            }
            catch (StorageException e)
            {
                _logger.LogError($"Failed to insert features, {e}");
                throw;
            }

            async Task InsertOneBatchAsync(CloudTable destTable, string entityType, int baseId, int batchSize)
            {
                // Create the batch operation. 
                TableBatchOperation batchOperation = new TableBatchOperation();

                // The following code  generates test data for use during the query samples.
                var id = baseId;
                for (int i = 0; i < batchSize; i++, id++)
                {
                    batchOperation.InsertOrMerge(new RecordFeatures(entityType, string.Format("{0}", id.ToString("D4"))) { });
                }

                // Execute the batch operation.
                IList<TableResult> results = await destTable.ExecuteBatchAsync(batchOperation);
            }
        }
    }
}
