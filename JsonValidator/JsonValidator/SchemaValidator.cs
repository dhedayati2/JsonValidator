using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;

namespace JsonValidator
{
    public class SchemaValidator
    {
        public string jsonFullFileName { get; set; }

        private string _schemaPath;
        public string SchemaPath
        {
            get
            {
                return _schemaPath;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Substring(value.Length - 1, 1) != @"\")
                        _schemaPath = value + @"\";
                    else
                        _schemaPath = value;
                }
            }
        }


        public IList<string> Validate()
        {
            if (string.IsNullOrEmpty(SchemaPath) || string.IsNullOrEmpty(jsonFullFileName))
                return null;

            IList<string> errors = new List<string>();
            //  using (StreamReader reader = File.OpenText(@"C:\Users\dhedayati\Documents\Visual Studio 2017\Projects\Repos\JsonValid\JsonValid\BDPSchema\business.json"))
            var parentSchema = Path.Combine(SchemaPath, "business.json");
            if (!File.Exists(parentSchema))
            {
                //System.Windows.MessageBox.Show("Schema file cannot be found.","Error");
                errors.Add("Schema file cannot be found");
                return errors;
            }

            using (StreamReader reader = File.OpenText(@"C:\Users\dhedayati\Documents\Visual Studio 2017\Projects\Repos\JsonValid\JsonValid\BDPSchema\business.json"))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                {
                    JSchemaUrlResolver resolver = new JSchemaUrlResolver();
                    JSchema schema = JSchema.Load(jsonReader, new JSchemaReaderSettings()
                    {
                        Resolver = resolver,

                        //BaseUri = new Uri(@"file://C:/Users/dhedayati/Documents/Visual Studio 2017/Projects/Repos/JsonValid/JsonValid/BDPSchema/")

                        BaseUri = new Uri(SchemaPath)
                    });

                    string data = ReadJsonFile();
                    var jTok = JToken.Parse(data);
                    jTok.IsValid(schema, out errors);
                }
            }
            return errors;
        }

        public string ReadJsonFile()
        {
            using (StreamReader reader = new StreamReader(@"C:\SageFinancialsDataDrop\Universl.json"))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
