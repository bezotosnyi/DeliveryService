namespace DeliveryService.UI
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Reflection;

    public static class Extensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> items) where T : class, new()
        {
            var dataTable = new DataTable(typeof(T).Name);
          
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in properties)
            {
                var columnName = propertyInfo.Name;

                if (propertyInfo.IsDefined(typeof(DisplayNameAttribute)))
                {
                    columnName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>().DisplayName;
                }

                dataTable.Columns.Add(columnName, propertyInfo.PropertyType);
            }

            foreach (var item in items)
            {
                if (item == null)
                {
                    continue;
                }

                var values = new object[properties.Length];
                for (var i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}