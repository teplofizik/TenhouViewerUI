using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Search
{
    class Search
    {
        // Параметры поиска
        public List<Query> Queries = new List<Query>();

        // Результаты
        public List<Result> GameList = new List<Result>();

        // Ищем по результатам предыдущих поисков
        public Search(List<Result> Results)
        {
            foreach (Result R in Results) GameList.Add(new Result(R));
        }

        // Добавить логические отношения между результатами проверок (&& || == !)
        public List<Result> Find()
        {
            List<Result> ResultList = new List<Result>();

            for (int i = 0; i < GameList.Count; i++)
            {
                Result R = GameList[i];

                // Проверяем
                foreach (Query Q in Queries) Q.Function(Q, R);

                // Если что-то нашлось, то добавляем
                if (R.Check()) ResultList.Add(R);
            }

            return ResultList;
        }
    }
}
