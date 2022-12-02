using System.Diagnostics;

namespace day02;

public static class Utils
{
    private static string GetMove(string move)
    {
        return move switch
        {
            "A" => "rock",
            "B" => "paper",
            "C" => "scissors",
            "X" => "rock",
            "Y" => "paper",
            "Z" => "scissors",
        };
    }
    
    private static int GetMoveScore(string move)
    {
        return move switch
        {
            "rock" => 1,
            "paper" => 2,
            "scissors" => 3,
        };
    }

    private static string GetOutcome(string outcome)
    {
        return outcome switch
        {
            "X" => "lose",
            "Y" => "draw",
            "Z" => "win",
        };
    }

    private static string GetMissingPlayerMove(string opponent, string outcome)
    {
        if (outcome == "win")
        {
            switch (opponent)
            {
                case "rock":
                    return "paper";
                case "paper":
                    return "scissors";
                case "scissors":
                    return "rock";
            }
        }
        else if (outcome == "lose")
        {
            switch (opponent)
            {
                case "rock":
                    return "scissors";
                case "paper":
                    return "rock";
                case "scissors":
                    return "paper";
            }
        }
        
        Debug.Assert(outcome.Contains("draw"));
        return opponent;
    }

    private static bool Draw(string opponent, string player)
    {
        return opponent == player;
    }
    
    public static int GetPlayerScore1(string opponent, string player)
    {
        opponent = GetMove(opponent);
        player = GetMove(player);
        
        switch (opponent)
        {
            // Lose
            case "rock" when player == "scissors":
            case "scissors" when player == "paper":
            case "paper" when player == "rock":
                return 0 + GetMoveScore(player);
            default:
            {
                if (Draw(opponent, player))
                {
                    return 3 + GetMoveScore(player);
                }
                else
                {
                    // Win
                    return 6 + GetMoveScore(player);
                }
            }
        }
    }

    public static int GetPlayerScore2(string opponent, string outcome)
    {
        opponent = GetMove(opponent);
        outcome = GetOutcome(outcome);
        
        var playerMove = GetMissingPlayerMove(opponent, outcome);

        return outcome switch
        {
            "lose" => 0 + GetMoveScore(playerMove),
            "draw" => 3 + GetMoveScore(playerMove),
            "win" => 6 + GetMoveScore(playerMove),
            _ => throw new Exception($"Invalid outcome {outcome}")
        };
    }
}