using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Current
{
    static Game game;
    public static Game GetGamePlaying {get{return game;}}
    public static Game SetGame{set{game = value;}}
}
