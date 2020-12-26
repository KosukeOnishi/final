using GameCanvas;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using System;
using System.Collections.Generic;

using GameTimer;
using GameLightTile;

/// <summary>
/// ゲームクラス。
/// 学生が編集すべきソースコードです。
/// </summary>
public sealed class Game : GameBase
{
    // 変数の宣言
    LightTile[] lights = new LightTile[25];
    List<LightTile> list = new List<LightTile>();

    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void InitGame()
    {
        // キャンバスの大きさを設定します
        gc.ChangeCanvasSize(1080, 1920);
        //ライトを5×5で25個分用意する
        System.Random rand = new System.Random();
        for (int i = 0; i < 25; i++)
        {
          int y = (((int)Math.Floor((double)i / 5)) * 180) + 500;
          int x = ((i % 5) * 180) + 100;
          bool isTurnedOn = rand.Next(0, 2) == 0 ? true : false;
          lights[i] = new LightTile(x, y, isTurnedOn);
        }
        list.AddRange(lights);
    }

    /// <summary>
    /// 動きなどの更新処理
    /// </summary>
    public override void UpdateGame()
    {
        // すべてのLIGHTに関して確認する処理
        foreach (LightTile light in lights)
        {
          float pointerX;
          float pointerY;
          //もしタップされていたら
          if (gc.IsTapped(light.x, light.y, light.length, light.length, out pointerX, out pointerY))
          {
            int index = list.IndexOf(light);
            light.OnPressed();
            if (index - 5 >= 0) { lights[index-5].OnPressed(); }//上のタイルをチェック
            if (index + 5 < 25) { lights[index+5].OnPressed(); }//下のタイルをチェック
            if (index % 5 != 0) { lights[index-1].OnPressed(); }//左のタイルをチェック
            if ((index + 1) % 5 != 0) { lights[index+1].OnPressed(); }//右のタイルをチェック
          }
        }
    }

    /// <summary>
    /// 描画の処理
    /// </summary>
    public override void DrawGame()
    {
        // 画面を白で塗りつぶします
        gc.ClearScreen();
        gc.SetBackgroundColor(gc.ColorGray);

        //タイトル
        gc.SetColor(255, 255, 255);
        gc.SetFontSize(80);
        gc.DrawString("LIGHTS OUT GAME", 100, 100);

        // ボックスを描画
        foreach (LightTile light in lights)
        {
          int val = light.isTurnedOn ? 255 : 0;
          gc.SetColor(val, val, val);
          gc.FillRect(light.x, light.y, light.length, light.length);
        }
    }
}
