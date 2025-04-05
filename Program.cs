using System;

namespace Sort_and_search_app_Asessment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Define the number of values the player must choose and the valid range
            int numValues = 6;   // The player must pick 6 numbers
            int minNum = 1;      // Minimum value allowed
            int maxNum = 49;     // Maximum value allowed

            // Arrays to store the player's numbers and randomly drawn numbers
            int[] playerNums = new int[numValues];
            int[] drawnNums = new int[numValues];

            // Create a random number generator
            Random rnd = new Random();


            // Step 1: Welcoming and selecting numbers

            Console.WriteLine("Welcome to\nTHE  LOTTERY GAME!!! ");
            Thread.Sleep(1500);
            Console.WriteLine($"Please enter {numValues} numbers between {minNum} and {maxNum}:");

            int count = 0; // Counter to track how many valid numbers the player has entered
            while (count < numValues)
            {
                Thread.Sleep(1000);
                Console.Write($"Number {count + 1}: "); // Prompt the user for input
                string input = Console.ReadLine(); // Read input from the player
                
                // Validate if input is a number
                if (int.TryParse(input, out int chosenNum))
                {
                    // Check if the number is within the valid range
                    if (chosenNum >= minNum && chosenNum <= maxNum)
                    {
                        // Check if the number is already chosen
                        if (!Array.Exists(playerNums, num => num == chosenNum))
                        {
                            playerNums[count] = chosenNum; // Store the valid number
                            count++; // Move to the next index
                        }
                        else
                        {
                            Console.WriteLine("Number already chosen. Try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Out of range. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Enter a valid number.");
                }
            }


            // Step 2: Generate unique random numbers

            int i = 0;  // Counter to track how many numbers have been generated
            while (i < numValues)
            {
                int randNum = rnd.Next(minNum, maxNum + 1); // Generate a random number

                // Ensure the number is unique before storing it
                if (!Array.Exists(drawnNums, num => num == randNum))
                {
                    drawnNums[i] = randNum; // Store the unique random number
                    i++; // Move to the next index
                }
            }

            // Sort drawn numbers to enable binary search
            Array.Sort(drawnNums);
            Array.Sort(playerNums);


            // Step 3: Display the drawn numbers and the player's numbers
            Console.WriteLine("\nYour numbers are:");
            foreach (int pnum in playerNums) // Print each number in the sorted player's array
            {
                Console.Write($"{pnum} ");
            } 

            Console.WriteLine("\nThe drawn numbers are:");
            
            foreach (int num in drawnNums) // Print each number in the sorted random array
            {
                Thread.Sleep(1000);
                Console.Beep(200, 1100);
                Console.Write($"{num} ");
            }
            Console.WriteLine("\n"); // New line after displaying numbers


            // Step 4: Check for matches using Binary Search and  check percentage of matches

            int matches = 0; // Variable to count the number of matches

            foreach (int num in playerNums) // Loop through the player's numbers
            {
                // Use BinarySearch function to check if the number is in the drawn numbers
                if (BinarySearch(drawnNums, num))
                {
                    matches++; // Increase match count if number is found
                }
            }


            // Display the number of matches
            Thread.Sleep(1000);
            Console.Beep(500, 1200);
            Console.WriteLine($"You matched {matches} number(s).");

            // Calculate the percentage of matches
            double percentage = (double)matches / numValues * 100; // Calculate percentage

            // Step 5: Display the lottery result

            if (percentage == 100)
            {
                Console.WriteLine("JACKPOT!!!!");
            }
            else if (percentage >= 50)
            {
                Console.WriteLine("Good Job!!! That was a close call.");
            }
            else if (percentage >= 25)
            {
                Console.WriteLine("Not bad! Keep trying.");
            }
            else
            {
                Console.WriteLine("Better luck next time!");
            }  
            
        }

        
        // Binary Search Function 
        static bool BinarySearch(int[] sortedNums, int target)
        {
            int left = 0;                     // Start of the search range
            int right = sortedNums.Length - 1; // End of the search range

            while (left <= right) // Continue searching as long as left does not exceed right
            {
                int mid = left + (right - left) / 2; // Find the middle index

                if (sortedNums[mid] == target)
                {
                    return true; // If the number is found, return true
                }
                else if (sortedNums[mid] < target)
                {
                    left = mid + 1; // Move search range to the right
                }
                else
                {
                    right = mid - 1; // Move search range to the left
                }
            }
            return false; // If number is not found, return false
        }

        // ==========================================
        // Linear Search Function 
        static bool LinearSearch(int[] numbers, int target)
        {
            foreach (int num in numbers) // Loop through the array
            {
                if (num == target) // Check if number matches the target
                {
                    return true; // Return true if the number is found
                }
            }
            return false; // Return false if the number is not found
        }
    }
}