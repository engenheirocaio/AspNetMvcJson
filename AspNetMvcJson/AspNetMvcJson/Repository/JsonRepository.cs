using Newtonsoft.Json;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AspNetMvcJson.Repository
{
    public class JsonRepository<T> : IRepository<T> where T : class, IEntidade
    {
        //private readonly string _path;
        private static readonly Dictionary<Type, string> Paths = new Dictionary<Type, string>();
        private readonly string _path;


        public JsonRepository(string caminhoBase)
        {
            var dir = caminhoBase;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            _path =  CriarCachePath(dir);
        } 

        private static string CriarCachePath(string dir)
        {
            var type = typeof(T);
            if(!Paths.ContainsKey(type))
                Paths.Add(type, Path.Combine(dir, $@"{type.Name}.json"));

            return Paths[type];
        }


        public List<T> GetAll()
        {
            var container = OpenJson();
            return container;
        }

        public T GetById(string id)
        {
            var container = OpenJson();
            return container.First(x => x.Id == id);
        }

        public List<T> GetBy(Func<T, bool> predicate)
        {
            var container = OpenJson();
            return container.Where(predicate).ToList();
        }

        public void Insert(T entidade)
        {
            var container = OpenJson();
            container.Add(entidade);
            SaveJson(container);
        }

        public void Delete(string id)
        {
            var container = OpenJson();

            container.RemoveAll(x => x.Id == id);
            SaveJson(container);
        }

        public void Update(T entidade)
        {

            var container = OpenJson();

            container.RemoveAll(x => x.Id == entidade.Id);
            container.Add(entidade);
            SaveJson(container);
        }



        private List<T> OpenJson()
        {
            lock (_path)
            {
                if (!File.Exists(_path))
                    return new List<T>()
                    {
                        
                    };

                return File.ReadAllText(_path).Deserialize<List<T>>();
            }
        }

        private void SaveJson(List<T> container)
        {
            lock (_path)
            {
                
                File.WriteAllText(_path, container.Serialize());
            }
        }
    }




    public interface IRepository
    {
    }

    public interface IRepository<T> : IRepository where T : IEntidade
    {
        List<T> GetAll();
        T GetById(string id);
        List<T> GetBy(Func<T, bool> predicate);

        void Insert(T entidade);
        void Delete(string id);
        void Update(T entidade);
    }

    public interface IEntidade
    {
        string Id { get; set; }
    }



    public static class Serializer
    {
        public static string Serialize<T>(this T obj)
        {

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Objects,
            };

            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.None });
        }

        public static T Deserialize<T>(this string obj)
        {

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Objects,
            };

            
            return JsonConvert.DeserializeObject<T>(obj, jsonSerializerSettings);
        }
    }
}