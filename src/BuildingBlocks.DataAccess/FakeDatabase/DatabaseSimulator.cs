namespace BuildingBlocks.DataAccess.FakeDatabase
{
    public class DatabaseSimulator
    {
        private readonly Dictionary<Type, object> _tables = new();

        public List<T> Set<T>() where T : class
        {
            if (!_tables.TryGetValue(typeof(T), out var list))
            {
                list = new List<T>();
                _tables[typeof(T)] = list;
            }

            return (List<T>)list;
        }
        public void Insert<T>(T entity) where T : class
        {
            var table = Set<T>();
            table.Add(entity);
        }
        public void InsertRange<T>(IEnumerable<T> entities) where T : class
        {
            var table = Set<T>();
            table.AddRange(entities);
        }
    }
}
