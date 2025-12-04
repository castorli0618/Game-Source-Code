using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Manages a list of coding questions and displays them one by one using the CodeInputUI system.
/// </summary>

public class QuestionManager : MonoBehaviour
{
    // defines a structure for a coding question with its accepted answers
    [Serializable]
    public struct CodeQuestion
    {
        public string question;           // question prompt to display
        public string[] acceptedAnswers;  // all accepted valid answers
    }

    // list of coding questions to ask
    public List<CodeQuestion> questions = new List<CodeQuestion>();

    private int currentIndex = 0; // tracks current question index

    void Awake()
    {
        // initialize questions when the game starts
        PopulateQuestions();
    }

    /// <summary>
    /// Shows the next coding question through the UI and handles the logic when it's answered correctly.
    /// </summary>
    /// <param name="onCorrect">callback function to execute on a correct answer</param>

    public void ShowNextQuestion(Action onCorrect)
    {
        // if all questions are completed
        if (currentIndex >= questions.Count)
        {
            Debug.Log("All questions completed!");
            onCorrect?.Invoke();
            return;
        }

        // show current question via CodeInputUI
        var question = questions[currentIndex];
        CodeInputUI.Instance.ShowUI(question.question, question.acceptedAnswers, () =>
        {
            currentIndex++;       // move to the next question
            onCorrect?.Invoke();  // callback trigger
        });
    }

    /// <summary>
    /// Populates the list of questions with their corresponding accepted answers.
    /// </summary>

    void PopulateQuestions()
    {
        questions = new List<CodeQuestion>
        {
            new CodeQuestion {
                question = "Print 'hello' in Python",
                acceptedAnswers = new string[] { "print('hello')", "print(\"hello\")" }
            },
            new CodeQuestion {
                question = "Assign the number 10 to a variable named x",
                acceptedAnswers = new string[] { "x = 10" }
            },
            new CodeQuestion {
                question = "Print the sum of 7 and 5",
                acceptedAnswers = new string[] { "print(7 + 5)", "print(12)" }
            },
            new CodeQuestion {
                question = "Check if 8 is greater than 3 using an if-statement",
                acceptedAnswers = new string[] { "if 8 > 3:", "if(8 > 3):" }
            },
            new CodeQuestion {
                question = "Create a list with 2, 4, 6 and assign to evens",
                acceptedAnswers = new string[] { "evens = [2, 4, 6]", "evens=[2,4,6]" }
            },
            new CodeQuestion {
                question = "Print the second item in the list evens",
                acceptedAnswers = new string[] { "print(evens[1])" }
            },
            new CodeQuestion {
                question = "Use len() to get the length of the list evens",
                acceptedAnswers = new string[] { "len(evens)", "print(len(evens))" }
            },
            new CodeQuestion {
                question = "Create a string variable name with value 'Bob'",
                acceptedAnswers = new string[] { "name = 'Bob'", "name=\"Bob\"" }
            },
            new CodeQuestion {
                question = "Print 'Hello, Bob' using the variable name",
                acceptedAnswers = new string[] { "print('Hello, ' + name)", "print(\"Hello, \" + name)" }
            },
            new CodeQuestion {
                question = "Write a while loop that runs while x < 3",
                acceptedAnswers = new string[] { "while x < 3:", "while(x < 3):" }
            },
            new CodeQuestion {
                question = "Increment x by 1 using shorthand",
                acceptedAnswers = new string[] { "x += 1" }
            },
            new CodeQuestion {
                question = "Check if 4 is in the list evens",
                acceptedAnswers = new string[] { "4 in evens", "print(4 in evens)" }
            },
            new CodeQuestion {
                question = "Create a dictionary with keys 'a':1 and 'b':2",
                acceptedAnswers = new string[] { "d = {'a': 1, 'b': 2}", "d={'a':1,'b':2}" }
            },
            new CodeQuestion {
                question = "Print the value of key 'a' in dictionary d",
                acceptedAnswers = new string[] { "print(d['a'])" }
            },
            new CodeQuestion {
                question = "Define a function add that returns x + y",
                acceptedAnswers = new string[] { "def add(x, y): return x + y" }
            },
            new CodeQuestion {
                question = "Call the function add with 3 and 4",
                acceptedAnswers = new string[] { "add(3, 4)", "print(add(3, 4))" }
            },
            new CodeQuestion {
                question = "Use a for loop to print numbers 0 to 2",
                acceptedAnswers = new string[] { "for i in range(3): print(i)" }
            },
            new CodeQuestion {
                question = "Convert the string '123' to an integer",
                acceptedAnswers = new string[] { "int('123')", "print(int('123'))" }
            },
            new CodeQuestion {
                question = "Check if number 10 is even",
                acceptedAnswers = new string[] { "10 % 2 == 0", "print(10 % 2 == 0)" }
            },
            new CodeQuestion {
                question = "Round 3.14159 to 2 decimal places",
                acceptedAnswers = new string[] { "round(3.14159, 2)", "print(round(3.14159, 2))" }
            }
        };
    }
}
