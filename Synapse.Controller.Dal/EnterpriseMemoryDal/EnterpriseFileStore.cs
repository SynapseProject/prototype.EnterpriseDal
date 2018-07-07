using System;
using Suplex.Security.AclModel.DataAccess.Utilities;
using Suplex.Utilities.Serialization;

namespace Synapse.Services.Controller.Dal
{
    public class EnterpriseFileStore : EnterpriseStore
    {
        //[YamlIgnore]
        public string CurrentPath { get; internal set; }


        public string ToYaml(bool serializeAsJson = false)
        {
            return YamlHelpers.Serialize( this,
                serializeAsJson: serializeAsJson, formatJson: serializeAsJson, emitDefaultValues: true, converter: new YamlAceConverter() );
        }

        public void ToYamlFile(string path = null, bool serializeAsJson = false)
        {
            if( string.IsNullOrWhiteSpace( path ) && !string.IsNullOrWhiteSpace( CurrentPath ) )
                path = CurrentPath;

            if( string.IsNullOrWhiteSpace( path ) )
                throw new ArgumentException( "path or CurrentPath must not be null." );

            YamlHelpers.SerializeFile( path, this,
                serializeAsJson: serializeAsJson, formatJson: serializeAsJson, converter: new YamlAceConverter() );

            CurrentPath = path;
        }

        public static EnterpriseFileStore FromYaml(string yaml)
        {
            return YamlHelpers.Deserialize<EnterpriseFileStore>( yaml, converter: new YamlAceConverter() );
        }

        public static EnterpriseFileStore FromYamlFile(string path)
        {
            EnterpriseFileStore store = YamlHelpers.DeserializeFile<EnterpriseFileStore>( path, converter: new YamlAceConverter() );
            store.CurrentPath = path;
            return store;
        }
    }
}