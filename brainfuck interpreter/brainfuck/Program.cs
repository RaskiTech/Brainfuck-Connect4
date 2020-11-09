using System;
using System.IO;

namespace brainfuck
{
    class Program
    {
        static string path = "";
        static byte[] memory = new byte[1000]; // Absolute max is 2147483647
        static int pointer = 0;

        static void Main(string[] args) {
            if (path == "") {
                Console.WriteLine("Input the code location:");
                path = Console.ReadLine();
            }

            text = ReadFile(path);
            for (int i = 0; i < text.Length; i++) {
                switch (text[i]) {
                    // Move the pointer to the right
                    case '>':
                        pointer++;
                        break;

                    // Move the pointer to the left
                    case '<':
                        pointer--;
                        break;

                    // Increment the byte in the point
                    case '+':
                        memory[pointer]++;
                        break;

                    // Decrement the byte at this point
                    case '-':
                        memory[pointer]--;
                        break;

                    // Jump to ] if current byte is 0
                    case '[':
                        if (memory[pointer] == 0) {
                            i++;
                            int layerCounter = 0;
                            while (text[i] != ']' || layerCounter > 0) {
                                // Make inner loops possible
                                if (text[i] == '[')
                                    layerCounter++;
                                else if (text[i] == ']')
                                    layerCounter--;
                                i++;
                            }
                        }
                        break;

                    // Jump to last [ if the current byte is not 0
                    case ']':
                        if (memory[pointer] != 0) {
                            i--;
                            int layerCounter = 0;
                            while (text[i] != '[' || layerCounter > 0) {
                                // Make inner loops possible
                                if (text[i] == ']')
                                    layerCounter++;
                                else if (text[i] == '[')
                                    layerCounter--;
                                i--;
                            }
                        }
                        break;

                    // Print the ASCII-value if the data pointer
                    case '.':
                        Console.Write(AsciiToText(memory[pointer]));
                        break;

                    // Take an input and store it as an ASCII-value
                    case ',':
                        memory[pointer] = TextToAscii(Console.ReadKey().KeyChar);
                        break;
                }
            }
            // Give the user time to see the result
            Console.WriteLine("\n-- Program Fisihed --\n");
            Console.Read();
        }

        static string ReadFile(string path) {
            if (Path.IsPathFullyQualified(path))
                return File.ReadAllText(path);
            else return "";
        }
        static byte TextToAscii(char character) { return Convert.ToByte(character); }
        static char AsciiToText(byte number) { return Convert.ToChar(number); }
    }
}
