using System.Text;

namespace IpCountry.Repository.Extensions
{
    public class QueryBuilder
    {
        public static void AppendListToQueryBuilder<T>(
    StringBuilder queryBuilder,
    IEnumerable<T> items,
    string columnName,
    IDictionary<string, object> parameters)
        {
            if (items == null || !items.Any())
            {
                return;
            }

            var parametersForCurrentItems = CreateParametersForItems(items, parameters.Count);

            parametersForCurrentItems.ToList().ForEach(parameters.Add);

            var parameterNames = parametersForCurrentItems.Select(p => p.Key);
            queryBuilder.Append($" {columnName} IN ({string.Join(", ", parameterNames)})");
        }

        private static IEnumerable<KeyValuePair<string, object>> CreateParametersForItems<T>(IEnumerable<T> items, int currentIndex)
        {
            var parameters = new List<KeyValuePair<string, object>>();

            foreach (var item in items)
            {
                if (item is null)
                {
                    continue;
                }

                var parameterName = "@value" + (++currentIndex);
                parameters.Add(new KeyValuePair<string, object>(parameterName, item));
            }

            return parameters;
        }
    }
}
