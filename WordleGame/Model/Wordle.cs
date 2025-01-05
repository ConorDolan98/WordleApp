using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleGame.Model
{
    public class Wordle
    {
        public string Word { get; set; }
    }
    public class ScoreData
    {
        public string PlayerName { get; set; }
        public string Word { get; set; }
        public int AttemptsUsed { get; set; }
    }
}
