using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGamePiecePicker
{
    void ApplyDamage();


    public enum GamePiece
    {
        Piece_1,
        Piece_2,
        Piece_3,
        Piece_4,
        Piece_5,
    }

    // Each piece has a probability between 0 - 100% (0.0 - 1.0)
    // Missing probability entries should be treated as 0 while
    // additional entries should be ignored.
    //
    // Assume that the probabilities always total up to 1.0
    void Init(List<float> probabilities);

    // Returns the next selected piece
    GamePiece GetNext();

  

}
