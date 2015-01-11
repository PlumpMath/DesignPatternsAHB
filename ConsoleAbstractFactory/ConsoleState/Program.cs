using System;

public class MainApp
{
    static void Main()
    {
        // Setup game in a state 
        var game = new Game(new ChallengerTurn());

        // state requests, which toggles state 
        Console.WriteLine("GameStates: " + game.State.State.ToString());

        game.ChangeTurn();
        Console.WriteLine("GameStates: " + game.State.State.ToString());

        game.ChangeTurn();
        Console.WriteLine("GameStates: " + game.State.State.ToString());

        game.ChangeTurn();
        Console.WriteLine("GameStates: " + game.State.State.ToString());

        // Wait for user 
        Console.Read();
    }
}

// "State" 
public abstract class StateGame
{
    public GameStates State { get; set; }
    public abstract void ChangeTurn(Game game, bool gameFinished);

}

public class ChallengerTurn: StateGame
{
    public override void ChangeTurn(Game game, bool gameFinished )
    {
        if (gameFinished)
        {
            game.State = new GameisFinished {State = GameStates.GameIsFinished};
        }
        else
        {
            game.State = new OpponentTurn {State = GameStates.OpponentTurn};
        }
    }
}

public class OpponentTurn : StateGame
{
    public override void ChangeTurn(Game game, bool gameFinished)
    {
        if (gameFinished)
        {
            game.State = new GameisFinished {State = GameStates.GameIsFinished};
        }
        else
        {
            game.State = new ChallengerTurn {State = GameStates.ChallengerTurn};
        }
    }
}

    public class GameisFinished : StateGame
    {
        public override void ChangeTurn(Game game, bool gameFinished)
        {
            game.State = new GameisFinished {State = GameStates.GameIsFinished};
        }
    }


// Game
 public class Game
{
    private StateGame state;

    // Constructor 
    public Game(StateGame state)
    {
        if (state == null)
            throw new ArgumentNullException("state");
        this.state = state;
    }

    // Property 
    public StateGame State
    {
        set{state = value;}
        get { return state; }
    }

    public void ChangeTurn()
    {
        // TODO: Check if game is finished
        bool gameFinished = false;
        state.ChangeTurn(this, gameFinished);
    }

}

public enum GameStates
{
    ChallengerTurn,
    OpponentTurn,
    GameIsFinished
}