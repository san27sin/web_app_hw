using Microsoft.Identity.Client;

namespace web_app_hw.Models
{  
    public class SampleObjectPool//примерные класс для тестирования
    {
        private static Lazy<SampleObjectPool> _instance
            = new Lazy<SampleObjectPool>(() => new SampleObjectPool(), true);

        public static SampleObjectPool Instance
        {
            get
            {
                return _instance.Value;
            }
        }


        private List<SampleObject> _objects;


        public SampleObjectPool()
        {
            _objects = new List<SampleObject>();
        }

        public bool Add(int id)
        {
            _objects.Add(new SampleObject
            {
                Id = id
            });
            return true;
        }

        public List<SampleObject> GetAll()
        {
            return _objects;
        }
    }
}
