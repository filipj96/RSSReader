﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Serialize
{
    class JSONSerializer<T> : ISerializers<T>
    {
        private JsonSerializer jsonSerializer;
        private JsonSerializerSettings settings;
        public JSONSerializer()
        {
            jsonSerializer = new JsonSerializer();
            settings = new JsonSerializerSettings { Formatting = Newtonsoft.Json.Formatting.Indented };
        }

        public void Serialize<T>(T serializeObject, string filePath, bool append, string fileName)
        {
            string jsonData = JsonConvert.SerializeObject(serializeObject, settings);

            using (StreamWriter writer = new StreamWriter(filePath + fileName + ".json"))
            {
                writer.Write(jsonData);
            }
        }
        public T Deserialize(string path)
        {
            T objectReturned;

            using(StreamReader reader = File.OpenText("SortedData.json"))
            {
                objectReturned = (T)jsonSerializer.Deserialize(reader, typeof(T));
            }

            return objectReturned;
        }

        public T[] DeserializeList()
        {
            throw new NotImplementedException();
        }

        public T[] SerializeList(List<T> list)
        {
            throw new NotImplementedException();
        }
    }
}