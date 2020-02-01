using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellTarget : MonoBehaviour
{
   public ShellLayer owner;

   private void OnMouseDown()
   {
      ShellPuzzleController.Instance.puzzle.PopLayer(owner);
   }
}
