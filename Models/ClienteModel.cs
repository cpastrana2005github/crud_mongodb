using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace crudmongo1.Models
{
    public class ClienteModel
    {
        [BsonId]
        public ObjectId cedulaCliente { get; set; }
        [BsonElement("nombreCliente")]
        public string nombreCliente { get; set; }
        [BsonElement("apellidoCliente")]
        public string apellidoCliente { get; set; }
    }
}