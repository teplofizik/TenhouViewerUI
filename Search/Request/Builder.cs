using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Search.Request
{
    class RequestItem
    {
        /// <summary>
        /// Название параметра поиска
        /// </summary>
        public string Name;

        /// <summary>
        /// Описание поля
        /// </summary>
        public string Description;

        /// <summary>
        /// Функция проверки условия
        /// </summary>
        public Action<Query, Result> Function;

        public RequestItem(string N, string D, Action<Query, Result> F)
        {
            Name = N;
            Description = D;
            Function = F;
        }
    }

    class Builder
    {
        private readonly RequestItem[] RequestList = {
            // Опции поиска игроков
            new RequestItem("nickname", "Ник игрока", Player.NickName),
            new RequestItem("rating", "Рейтинг игрока", Player.Rating),
            new RequestItem("rank", "Ранг игрока", Player.Rank),
            new RequestItem("sex", "Пол игрока (m, f)", Player.Sex),
            // Результат игры
            new RequestItem("balance", "Счёт игрока на конец игры", ReplayResult.Balance),
            new RequestItem("result", "Результат игры с учётом оки и умы", ReplayResult.Result),
            new RequestItem("place", "Занятое место", ReplayResult.Place),
            // Результат раздачи

        };
    }
}
