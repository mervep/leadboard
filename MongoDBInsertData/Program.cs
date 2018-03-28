using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace MongoDBInsertData
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("mygamers");
            var collect = database.GetCollection<BsonDocument>("gamers");
            var fileName = "ornekdata1.json";

            using (var streamReader = new StreamReader(fileName))
            {
                string line;
                string jsonLine="";
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.Trim() !="},"){
                    jsonLine += line.Trim();
                }
                    else if(line.Trim() == "},")
                    {
                        jsonLine += "}";
                        using (var jsonReader = new JsonReader(jsonLine))
                        {
                            var serializer = new BsonArraySerializer();
                            var document = BsonDocument.Parse(jsonLine);
                            collect.InsertOne(document);
                            jsonLine = "";
                        }
                    }

                    
                }
            }

        }
    }
}
