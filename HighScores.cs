using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace qbert
{
    public class HighScores
    {
        int[] Scores;
        string[] Users;

        public HighScores()
        {
            Scores = new int[10];
            Users = new string[10];

            for (int i = 0; i < Scores.Length; i++)
            {
                Scores[i] = 0;
                Users[i] = "";
            }
        }

        public void addHighScore(int s, string u)
        {
            int tempScore = 0;
            string tempUser = "";

            for (int i = 0; i < Scores.Length; i++)
            {

                if (Scores[i] <= s)
                {
                    tempScore = Scores[i];
                    tempUser = Users[i];

                    Scores[i] = s;
                    Users[i] = u;

                    s = tempScore;
                    u = tempUser;
                }
            }
            try
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter("scores.txt");
                writer.Write(showHighScores());
                writer.Flush();
                writer.Close();
            }catch(Exception ex){
                MessageBox.Show(ex.Message);
            }

        }

        public string showHighScores()
        {
            string output = "";
            for (int i = 0; i < Scores.Length; i++)
            {
                output += Users[i] + "\r\n " + Scores[i].ToString() + "\r\n";
            }
            MessageBox.Show(output);
            return output;
        }
        public void getHighScores()
        {
            try
            {
                System.IO.StreamReader reader = new System.IO.StreamReader("scores.txt");
                int counter = 0;
                while (!reader.EndOfStream)
                {
                    if (counter < Scores.Length)
                    {
                        Users[counter] = reader.ReadLine();
                        int.TryParse(reader.ReadLine(), out Scores[counter]);
                        counter++;
                    }
                }
                reader.Close();

            } catch (Exception ex)
            {
               // MessageBox.Show("Error: " + ex.Message + "\n" + "Don't worry, I got this");
            }
        }
    }
}
