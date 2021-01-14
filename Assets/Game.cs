using GameCanvas;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using System;
using System.Collections.Generic;

using GameTimer;
using GameLightTile;
using DataSet;

public enum GameState
{
  Home,
  Guide,
  Playing,
  Clear
}

/// <summary>
/// ゲームクラス。
/// 学生が編集すべきソースコードです。
/// </summary>
public sealed class Game : GameBase
{
    // 変数の宣言
    LightTile[] lights = new LightTile[16];
    List<LightTile> list = new List<LightTile>();
    int step = 0;
    System.Random rand = new System.Random();
    Timer timer = new Timer();
    int bestTime = 0;
    int bestStep = 0;
    GameState currentGameState = GameState.Home;

    //タイルの初期化
    public void InitTiles()
    {
      timer.Reset();
      timer.Start();
      step = 0;
      string data = Data.dataset[rand.Next(0, 4097)];
      for (int i = 0; i < 16; i++)
      {
        int y = (((int)Math.Floor((double)i / 4)) * 230) + 500;
        int x = ((i % 4) * 230) + 100;
        string dataFlag = i == 15 ? data.Substring(i) : data.Substring(i, 1);
        bool isTurnedOn =  dataFlag == "1" ? true : false;
        lights[i] = new LightTile(x, y, isTurnedOn);
      }
      list = new List<LightTile>();
      list.AddRange(lights);
    }

    //タイルのリセット
    public void ResetTiles()
    {
      step = 0;
      foreach (LightTile light in lights)
      {
        light.Reset();
      }
    }

    public void GameClear()
    {
      if (bestTime == 0 || timer.current < bestTime)
      {
        bestTime = timer.current;
        bestStep = step;
      }
      timer.Stop();
      currentGameState = GameState.Clear;
    }

    public void BackToHome()
    {
      currentGameState = GameState.Home;
      timer.Reset();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void InitGame()
    {
        // キャンバスの大きさを設定します
        gc.ChangeCanvasSize(1080, 1920);
    }

    /// <summary>
    /// 動きなどの更新処理
    /// </summary>
    public override void UpdateGame()
    {
        if (currentGameState == GameState.Home)
        {
          float pointerX;
          float pointerY;
          if (gc.IsTapped(400, 1300, 300, 120, out pointerX, out pointerY))
          {
            currentGameState = GameState.Playing;
            timer.Start();
            InitTiles();
          }
          if (gc.IsTapped(400, 1500, 300, 120, out pointerX, out pointerY))
          {
            currentGameState = GameState.Guide;
          }
        }
        else if (currentGameState == GameState.Guide)
        {
          float pointerX;
          float pointerY;
          if (gc.IsTapped(100, 300, 100, 100, out pointerX, out pointerY))
          {
            BackToHome();
          }
          if (gc.IsTapped(300, 1700, 500, 100, out pointerX, out pointerY))
          {
            currentGameState = GameState.Playing;
            timer.Start();
            InitTiles();
          }
        }
        else if (currentGameState == GameState.Playing)
        {
          timer.Update();
          int clearFlag = 0;
          // すべてのLIGHTに関して確認する処理
          foreach (LightTile light in lights)
          {
            float pointerX;
            float pointerY;
            //もしタイルがタップされていたら
            if (gc.IsTapped(light.x, light.y, light.length, light.length, out pointerX, out pointerY))
            {
              step++;
              int index = list.IndexOf(light);
              light.OnPressed();
              if (index - 4 >= 0) { lights[index-4].OnPressed(); }//上のタイルをチェック
              if (index + 4 < 16) { lights[index+4].OnPressed(); }//下のタイルをチェック
              if (index % 4 != 0) { lights[index-1].OnPressed(); }//左のタイルをチェック
              if ((index + 1) % 4 != 0) { lights[index+1].OnPressed(); }//右のタイルをチェック
            }

            if (gc.IsTapped(100, 300, 100, 100, out pointerX, out pointerY))
            {
              BackToHome();
            }

            if (gc.IsTapped(100, 1720, 200, 100, out pointerX, out pointerY))
            {
              InitTiles();
            }

            if (gc.IsTapped(400, 1720, 200, 100, out pointerX, out pointerY))
            {
              ResetTiles();
            }

            if (light.isTurnedOn)
            {
              clearFlag++;
            }
          }

          //clearFlagを確認（0だったらクリア）
          if (clearFlag == 0)
          {
            GameClear();
          }
        }
        else if (currentGameState == GameState.Clear)
        {
          float pointerX;
          float pointerY;
          //戻るボタン
          if (gc.IsTapped(100, 300, 100, 100, out pointerX, out pointerY))
          {
            BackToHome();
          }
          //もう一度遊ぶ
          if (gc.IsTapped(300, 1720, 400, 100, out pointerX, out pointerY))
          {
            currentGameState = GameState.Playing;
            InitTiles();
          }
        }
    }

    /// <summary>
    /// 描画の処理
    /// </summary>
    public override void DrawGame()
    {
        // 画面を灰色で塗りつぶします
        gc.ClearScreen();
        gc.SetBackgroundColor(gc.ColorGray);

        //タイトル
        gc.SetColor(255, 255, 255);
        gc.SetFontSize(80);
        gc.DrawString("LIGHTS OUT GAME", 100, 100);

        if (currentGameState == GameState.Home)
        {
          gc.DrawImage(GcImage.LightsOutLogo, 220, 400);
          gc.DrawString("START", 400, 1300);
          gc.DrawString("遊び方", 400, 1500);
        }
        else if (currentGameState == GameState.Guide)
        {
          gc.DrawString("←", 100, 300);
          gc.DrawString("遊び方", 400, 300);
          gc.DrawImage(GcImage.Example1, 200, 500);
          gc.DrawImage(GcImage.Example2, 600, 500);
          gc.DrawString("ライトをタップすると、\nそのライトと、隣接する\nライトのON/OFFが\n反転します。", 100, 900);
          gc.DrawString("すべてのライトを消すと\nゲームクリアです。", 100, 1400);
          gc.DrawString("さっそく遊ぶ", 300, 1700);
        }
        else if (currentGameState == GameState.Playing)
        {
          gc.DrawString("←", 100, 300);
          gc.DrawString(timer.current.ToString()+"s", 840, 300);
          if (bestTime != 0 && bestStep != 0)
          {
            gc.SetColor(20, 20, 255);
            gc.DrawString("BEST: " + bestTime.ToString() + "s STEP: " + bestStep.ToString(), 100, 1520);
          }
          gc.SetColor(255, 255, 255);
          gc.DrawString("RELOAD", 100, 1720);
          gc.DrawString("RESET", 400, 1720);
          gc.DrawString("STEP: "+step.ToString(), 700, 1720);
          // ボックスを描画
          foreach (LightTile light in lights)
          {
            int val = light.isTurnedOn ? 255 : 0;
            gc.SetColor(val, val, val);
            gc.FillRect(light.x, light.y, light.length, light.length);
          }
        }
        else //クリア画面
        {
          gc.DrawString("←", 100, 300);
          gc.DrawString(timer.current.ToString()+"s", 840, 300);
          gc.DrawImage(GcImage.DoneIcon, 220, 500);
          gc.DrawString("ゲームクリア！", 300, 1200);
          gc.DrawString(timer.current.ToString()+"s", 300, 1400);
          gc.DrawString("STEP: " + step.ToString(), 500, 1400);
          gc.SetColor(20, 20, 255);
          gc.DrawString("BEST: " + bestTime.ToString() + "s (STEP: " + bestStep.ToString() + ")", 200, 1520);
          gc.SetColor(255, 255, 255);
          gc.DrawString("もう一度遊ぶ", 300, 1720);
        }
    }
}
