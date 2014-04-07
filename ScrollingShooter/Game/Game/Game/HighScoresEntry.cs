using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter
{
    
    [Serializable]
    class HighScoresEntry
    {
        public int score;
        public string name;

        public HighScoresEntry(int score, string name)
        {
            this.score = score;
            this.name = name;
        }
    }
}
