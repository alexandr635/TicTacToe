using System;
using System.Windows.Controls;

namespace Logic
{
    public class Bot
    {
        private static Button[,] buttonArray { set; get; }
        private static byte size { set; get; }

        private static void ActHorizontal(sbyte i)
        {
            for (sbyte j = 0; j < size; j++)
            {
                if (buttonArray[i, j].Content == null)
                {
                    buttonArray[i, j].Content = "o";
                    return;
                }
            }
            RandomStep();
        }

        private static void ActVertical(sbyte i)
        {
            for (byte j = 0; j < size; j++)
            {
                if (buttonArray[j, i].Content == null)
                {
                    buttonArray[j, i].Content = "o";
                    return;
                }
            }
            RandomStep();
        }

        private static void ActDiagonal1()
        {
            for (byte i = 0; i < size; i++)
            {
                for (byte j = 0; j < size; j++)
                {
                    if (i == j && buttonArray[i, j].Content == null)
                    {
                        buttonArray[i, j].Content = "o";
                        return;
                    }
                }
            }
            RandomStep();
        }

        private static void ActDiagonal2()
        {
            for (byte i = 0; i < size; i++)
            {
                for (byte j = 0; j < size; j++)
                {
                    if (i + j == size - 1 && buttonArray[i, j].Content == null)
                    {
                        buttonArray[i, j].Content = "o";
                        return;
                    }
                }
            }
            RandomStep();
        }

        private static void CleverActBot()
        {
            sbyte win, lose, place = -1;

            //проверка на горизонталь
            for (sbyte i = 0; i < size; i++)
            {
                win = lose = 0;
                for (sbyte j = 0; j < size; j++)
                {
                    if ((string)buttonArray[i, j].Content == "x")
                    {
                        win++;
                        lose = 0;
                    }
                    else if ((string)buttonArray[i, j].Content == "o")
                    {
                        lose++;
                        win = 0;
                    }
                }
                if (lose == size - 1)
                {
                    ActHorizontal(i);
                    return;
                }
                else if (win == size - 1 && place == -1)
                    place = i;
            }

            if (place != -1)
            {
                ActHorizontal(place);
                return;
            }

            place = -1;
            //проверка на вертикаль
            for (sbyte i = 0; i < size; i++)
            {
                win = lose = 0;
                for (sbyte j = 0; j < size; j++)
                {
                    if ((string)buttonArray[j, i].Content == "x")
                    {
                        win++;
                        lose = 0;
                    }
                    else if ((string)buttonArray[j, i].Content == "o")
                    {
                        lose++;
                        win = 0;
                    }
                }
                if (lose == size - 1)
                {
                    ActVertical(i);
                    return;
                }
                else if (win == size - 1 && place == -1)
                    place = i;
            }

            if (place != -1)
            {
                ActVertical(place);
                return;
            }

            win = lose = 0;
            //проверка на диагональ сверху вниз
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                        if ((string)buttonArray[j, i].Content == "x")
                            win++;
                        else if ((string)buttonArray[j, i].Content == "o")
                            lose++;
                    }
                }
            }

            if (lose == size - 1 || win == size - 1)
            {
                ActDiagonal1();
                return;
            }

            win = lose = 0;
            //проверка на диагональ снизу вверх
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i + j == size - 1)
                    {
                        if ((string)buttonArray[j, i].Content == "x")
                            win++;
                        else if ((string)buttonArray[j, i].Content == "o")
                            lose++;
                    }
                }
            }

            if (lose == size - 1 || win == size - 1)
            {
                ActDiagonal2();
                return;
            }

            RandomStep();
        }

        private static void RandomStep()
        {
            bool flag = false;
            Random rand = new Random();
            do
            {
                byte i = (byte)rand.Next(0, size);
                byte j = (byte)rand.Next(0, size);
                if (buttonArray[i, j].Content != null)
                    continue;
                else
                {
                    buttonArray[i, j].Content = "o";
                    flag = true;
                }

            } while (flag == false);
        }

        public static Button[,] BotAct(Button[,] arrayOfButton)
        {
            buttonArray = arrayOfButton;
            size = (byte)Math.Sqrt(buttonArray.Length);

            CleverActBot();
            return buttonArray;
        }
    }
}
