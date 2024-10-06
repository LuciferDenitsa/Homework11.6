using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Homework11._6.Services;

namespace Homework11._6
{
    internal static class Summator
    {
        internal static string Sum(Message message)
        {
            double sum = 0;
            string text = message.Text;
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (double.TryParse(word, out double number))
                    sum += number;
            }
            if (sum > 0)
                return ($"Сумма введенных чисел = {sum}");
            else
                return ("Вы не ввели числа");


        }
    }
}
