/*
 * Purpose: To create a "hangman" game that picks a random word from a text file of words that then requests the user to 
 * guess each letter of the word. 
 * 
 * Input: User input of a letter to guess each letter of the word
 * 
 * Process: When the user makes a correct guess, the actual letter is then displayed. When the user finishes a word,
 * it will display the number of misses and asks the user whether to continue to play with another word.
 * 
 * Output: If the user makes a correct guess, the actual letter is displayed, if not it remains as an asterisk until the user
 * guesses correctly. Once word is correctly guessed, it will prompt user to play again or not.
 * 
 * Author: Kevin Tran
 * 
 * Last Modified: November 30, 2019
 */
using System;
using System.Collections.Generic;
using System.IO;
namespace AdvancedPortfolio01_KevinTran
{
    class HangmanGame
    {
        static void LoadData(string fileName, List<String> nameList)
        {
            // Using the streamreader to read each line in a text file and storing it as an item in an array
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        // Create array to store the string
                        string[] lineArray = {line};
                        string name = lineArray[0];
                        nameList.Add(name);
                    }                       
                }
            }
            catch (Exception invalidException)
            {
                Console.WriteLine("The file could not be read.");
                Console.WriteLine(invalidException.Message);
            }
        } // End of load data method

        static char[] HangmanMysteryWord(string mysteryWord)
        {
            // Creates a string to capture a random generated word from the text file list array
            char[] hangManGuess = new char[mysteryWord.Length];
            for (int mysteryWordIndex = 0; mysteryWordIndex < mysteryWord.Length; mysteryWordIndex++)
            {
                hangManGuess[mysteryWordIndex] = '*';               
            }

            return hangManGuess;
        } // End of Hangman mystery word method

        static void WelcomeMessage()
        {
            Console.WriteLine("|-----------------------------------|");
            Console.WriteLine("| CPSC1012 Hangman Game             |");
            Console.WriteLine("|-----------------------------------|");
        } // End of Welcome message method

        static void BeginGame()
        {
            Console.WriteLine("I have picked a random word on Programming.");
            Console.WriteLine("Your task is to guess the correct word.");
            Console.WriteLine();
        } // End of Begin game method

        static int RandomizeMysteryWords()
        {
            // Generates a random number
            Random randGen = new Random();
            int wordGenerator = randGen.Next(0, 9);

            return wordGenerator;
        } // End of Randomize mystery words method

        static void DisplayEnterLetters(char[] hiddenMysteryLetters)
        {
            // Displays the prompt message to the user
            Console.Write("(Guess) Enter a letter in the word ");
            DisplayMysteryWord(hiddenMysteryLetters);
            Console.Write(" : ");
        } // End of Display enter letters method

        static void DisplayMysteryWord(char[] letterArray)
        {
            // Displaying the mystery word
            foreach(char letter in letterArray)
            {
                Console.Write(letter);
            }
        } // End of Display mystery word method

        static char ValidateHangmanWord(string mysteryWord, char[] hiddenMysteryLetters)
        {
            // Validates whether or not the character is already in the word
            string usedLetter = String.Empty;
            int missedGuess = 0;
            char playAgain = '\0';
            bool gameOver = false;
            while (!gameOver)
            {
                char animalNameSearch = Console.ReadKey().KeyChar;
                animalNameSearch = char.ToLower(animalNameSearch);
                Console.WriteLine();

                if (!Char.IsLetter(animalNameSearch))
                {
                    Console.WriteLine($"{animalNameSearch} is not a letter");
                }

                if (mysteryWord.Contains(animalNameSearch))
                {
                    for (int index = 0; index < mysteryWord.Length; index++)
                    {
                        if (animalNameSearch == mysteryWord[index])
                        {
                            hiddenMysteryLetters[index] = animalNameSearch;
                            if (!usedLetter.Contains(animalNameSearch))
                            {
                                usedLetter += animalNameSearch;
                            }
                            else
                            {
                                Console.WriteLine($"{animalNameSearch} is already in the word");
                            }
                        }                      
                    }
                }
                else
                {
                    Console.WriteLine($"{animalNameSearch} is not in the word");
                    missedGuess += 1;
                }
               
                if (usedLetter == mysteryWord)
                {
                    gameOver = true;
                    // Asks the user and returns a response of y to play again or n to stop
                    playAgain = GameResults(mysteryWord, missedGuess);
                }
                else
                {
                    DisplayEnterLetters(hiddenMysteryLetters);
                }              
            } // End of while loop      

            return playAgain;
        } // End of Validate hangman word method

        static char GameResults(string mysteryWord, int missedGuess)
        {
            char playAgainDecision = '\0';
            bool playAgain = false;            

            while (!playAgain)
            {
                Console.WriteLine($"The word is {mysteryWord}. You missed {missedGuess}");
                Console.Write("Do you want to guess another word? Enter y or n: \n");                
                try
                {
                    playAgainDecision = Console.ReadKey().KeyChar;
                    if (playAgainDecision == 'y' || playAgainDecision == 'Y')
                    {
                        playAgain = true;
                    }
                    else if(playAgainDecision == 'n' || playAgainDecision == 'N')
                    {
                        playAgain = true;
                    }
                    else
                    {
                        Console.WriteLine($"{playAgainDecision} is not a valid choice. Please try again");
                    }
                }
                catch
                {
                    Console.WriteLine($"{playAgainDecision} is not a valid choice");
                }
            }
            
            return playAgainDecision;
        } // End of Game results method

        static void Main(string[] args)
        {
            // Reads the text file from location and stores it in a list
            const string FileName = @"C:\Intel\animals.txt";
            List<string> nameList = new List<string>();
            LoadData(FileName, nameList);

            WelcomeMessage();
            char repeat;
            do
            {
                Console.WriteLine();
                BeginGame();
                int wordGenerator = RandomizeMysteryWords();
                string mysteryWord = nameList[wordGenerator].ToLower();
                char[] hiddenMysteryLetters = HangmanMysteryWord(mysteryWord);

                DisplayEnterLetters(hiddenMysteryLetters);
                repeat = ValidateHangmanWord(mysteryWord, hiddenMysteryLetters);
                
                if(repeat == 'n' || repeat == 'N')
                {
                    Console.WriteLine("\nGood-bye and thanks for playing the Hangman game");
                }

            } while (repeat == 'y' || repeat == 'Y');
        }
    }
}
