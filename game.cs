using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class game : MonoBehaviour
{
    public Tilemap pole, pole_hole, pole_hole_dop, pole_wumpus, pole_bat_dop, pole_bat, pole_player;
    public Tile player_l,player_r,bat,fon,fon_bat,fon_hole,fon_wumpus,hole,wumpus,up_block, down_block, left_block, right_block;
    public GameObject frost,hot,wint,over,attack_game,move_game, заново, buttons,pole_game;
    public TMPro.TMP_Text TEXT, new_text,bat_text;
    bool end = true;
    bool rotate = true;
    bool peregenerated = false;
    public AudioSource sound,sound_2,test_2,bats;
    bool up, down, left, right, up_1, down_1, left_1, right_1, test;
    string[][] MAP = new string[LORD.LEN][];
    int X, Y, X_player, Y_player, X_wumpus, Y_wumpus, half_len, count_moves;
    int стрелочки = LORD.стрелочки;
    int WASD=LORD.WASD;
    int dop=0;
    void Start()
    {
        if (LORD.экранные_стрелочки==3)
        {
            move_game.SetActive(false);
            attack_game.SetActive(true);
        }
        pole_game.transform.localScale = new Vector3(864/LORD.LEN, 864 / LORD.LEN, 864 / LORD.LEN);
        half_len=LORD.LEN / 2;
        if (LORD.LEN % 2 == 0)
        {
            for (int i = -half_len; i < half_len; i++)
            {
                pole.SetTile(new Vector3Int(i, half_len, 0), up_block);
                pole.SetTile(new Vector3Int(i, -half_len - 1, 0), down_block);
                pole.SetTile(new Vector3Int(-half_len - 1, i, 0), left_block);
                pole.SetTile(new Vector3Int(half_len, i, 0), right_block);
            }
        }
        else
        {
            pole_game.transform.localPosition = new Vector3(-(864 / LORD.LEN / 2), -(864 / LORD.LEN / 2), 0);
            for (int i = -half_len; i < half_len+1; i++)
            {
                pole.SetTile(new Vector3Int(i, half_len+1, 0), up_block);
                pole.SetTile(new Vector3Int(i, -half_len - 1, 0), down_block);
                pole.SetTile(new Vector3Int(-half_len - 1, i, 0), left_block);
                pole.SetTile(new Vector3Int(half_len+1, i, 0), right_block);
            }
        }
            test = false;
        bats.volume = LORD.слайдер * 0.7f;
        sound.volume = LORD.слайдер * 0.5f;
        sound_2.volume = LORD.слайдер * 0.4f;
        if (LORD.экранные_стрелочки != 2)
        {
            buttons.SetActive(true);
        }
        for (int i = 0; i < LORD.LEN; i++)
        {
            MAP[i] = new string[LORD.LEN];
            for (int r = 0; r < LORD.LEN; r++)
            {
                MAP[i][r] = ".";
            }
        }
        generate(1, "P", "p", new string[0] { }, false);
        X_player = X;
        Y_player = Y;
        generate(1, "W", "w", new string[2] { "P", "p" }, true);
        X_wumpus = X;
        Y_wumpus = Y;
        generate(LORD.HOLE, "H", "h", new string[4] { "H", "P", "p", "W" }, false);
        generate(LORD.BAT, "B", "b", new string[4] { "B", "P", "p", "W" }, false);
        render();
        pole_player.SetTile(new Vector3Int(X_player - LORD.LEN / 2, Y_player - LORD.LEN / 2, 0), player_r);
    }


    void Update()
    {
        if (end)
        {//хотьба
            move(KeyCode.UpArrow, KeyCode.W, up, 0, 1, "");
            move(KeyCode.LeftArrow, KeyCode.A, left, -1, 0, "left");
            move(KeyCode.DownArrow, KeyCode.S, down, 0, -1, "");
            move(KeyCode.RightArrow, KeyCode.D, right, 1, 0, "right");
            attack(KeyCode.UpArrow, KeyCode.W, up_1, 0, 1);
            attack(KeyCode.LeftArrow, KeyCode.A, left_1, -1, 0);
            attack(KeyCode.DownArrow, KeyCode.S, down_1, 0, -1);
            attack(KeyCode.RightArrow, KeyCode.D, right_1, 1, 0);
            if (peregenerated)
            {
                bats.Play();
                bat_text.text= bat_text.text+"ШШшш - летучие мыши ищют новый дом -ШШШшш"[dop];
                dop++;
                if (dop>= "ШШшш - летучие мыши ищют новый дом -ШШШшш".Length)
                {
                    peregenerated = false;
                    Invoke("del_text",4f);
                    dop = 0;
                }
            }
        }
    }
    void render()
    {
        frost.SetActive(false);
        wint.SetActive(false);
        hot.SetActive(false);
        if(pole.GetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0)) == null && end)
        {
            sound.Play();
        }
        pole.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), fon);
        if (MAP[X_player][Y_player].Contains("h"))
        {
            pole_hole_dop.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), fon_hole);
            frost.SetActive(true);
        }
        if (MAP[X_player][Y_player].Contains("b"))
        {
            pole_bat_dop.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), fon_bat);
            wint.SetActive(true);
        }
        if (MAP[X_player][Y_player].Contains("w"))
        {
            pole_wumpus.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), fon_wumpus);
            hot.SetActive(true);
        }
        if (MAP[X_player][Y_player].Contains("B"))
        {
            pole_bat.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), bat);
            pole_player.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), null);
            if (MAP[X_player][Y_player].Contains("H")) 
            { 
                pole_hole.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), hole);
            }
            X_player = Random.Range(0,LORD.LEN);
            Y_player = Random.Range(0, LORD.LEN);
            if (rotate)
            {
                pole_player.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), player_r);
            }
            else
            {
                pole_player.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), player_l);
            }
            render();
        }
        if (MAP[X_player][Y_player].Contains("H"))
        {
            pole_hole.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), hole);
            if (end)
            {
                GameOver();
                TEXT.text = "Вы упали в яму";
            }
        }
        if (MAP[X_player][Y_player].Contains("W"))
        {
            pole_wumpus.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), wumpus);
            if (end)
            {
                GameOver();
                TEXT.text = "Вас съели";
            }
        }
    }
    void GameOver()
    {
        X_player = X_wumpus;
        Y_player = Y_wumpus;
        end = false;
        render();
        over.SetActive(true);
        заново.SetActive(true);
        sound_2.Play();
        frost.SetActive(false);
        wint.SetActive(false);
        hot.SetActive(false);
    }
    public void атака()
    {
        test = !test;
        test_2.Play();
    }
        public void вверх()
    {
        if (LORD.экранные_стрелочки == 1 || test && LORD.экранные_стрелочки == 3)
        {
            up_1 = true;
        }
        else if (LORD.экранные_стрелочки == 0|| LORD.экранные_стрелочки == 3)
        {
            up = true;
        }
    }
    public void вниз()
    {
        if (LORD.экранные_стрелочки == 1 || test && LORD.экранные_стрелочки == 3)
        {
            down_1 = true;
        }
        else if (LORD.экранные_стрелочки == 0 || LORD.экранные_стрелочки == 3)
        {
            down = true;
        }
    }
    public void влево()
    {
        if (LORD.экранные_стрелочки == 1 || test && LORD.экранные_стрелочки == 3)
        {
            left_1 = true;
        }
        else if (LORD.экранные_стрелочки == 0 || LORD.экранные_стрелочки == 3)
        {
            left = true;
        }
    }
    public void вправо()
    {
        if (LORD.экранные_стрелочки == 1 || test && LORD.экранные_стрелочки == 3)
        {
            right_1 = true;
        }
        else if (LORD.экранные_стрелочки == 0 || LORD.экранные_стрелочки == 3)
        {
            right = true;
        }
    }
    void generate(int _count, string _unit, string _dop, string[] _block, bool big)
    {
        for (; _count > 0;)
        {
            X = Mathf.RoundToInt(Random.Range(0, LORD.LEN));
            Y = Mathf.RoundToInt(Random.Range(0, LORD.LEN));
            if (_block.Any(MAP[X][Y].Contains) != true && MAP[X][Y].Contains(_unit) != true)
            {
                MAP[X][Y] += _unit;
                MAP[(X + LORD.LEN) % LORD.LEN][(Y - 1 + LORD.LEN) % LORD.LEN] += _dop;
                MAP[(X + LORD.LEN) % LORD.LEN][(Y + 1 + LORD.LEN) % LORD.LEN] += _dop;
                MAP[(X - 1 + LORD.LEN) % LORD.LEN][(Y + LORD.LEN) % LORD.LEN] += _dop;
                MAP[(X + 1 + LORD.LEN) % LORD.LEN][(Y + LORD.LEN) % LORD.LEN] += _dop;
                if (big)
                {
                    MAP[(X + 1 + LORD.LEN) % LORD.LEN][(Y + 1 + LORD.LEN) % LORD.LEN] += _dop;
                    MAP[(X + 1 + LORD.LEN) % LORD.LEN][(Y - 1 + LORD.LEN) % LORD.LEN] += _dop;
                    MAP[(X - 1 + LORD.LEN) % LORD.LEN][(Y + 1 + LORD.LEN) % LORD.LEN] += _dop;
                    MAP[(X - 1 + LORD.LEN) % LORD.LEN][(Y - 1 + LORD.LEN) % LORD.LEN] += _dop;
                    MAP[(X - 2 + LORD.LEN) % LORD.LEN][(Y + LORD.LEN) % LORD.LEN] += _dop;
                    MAP[(X + 2 + LORD.LEN) % LORD.LEN][(Y + LORD.LEN) % LORD.LEN] += _dop;
                    MAP[(X + LORD.LEN) % LORD.LEN][(Y - 2 + LORD.LEN) % LORD.LEN] += _dop;
                    MAP[(X + LORD.LEN) % LORD.LEN][(Y + 2 + LORD.LEN) % LORD.LEN] += _dop;
                }
                _count--;
            }
        }
    }
    //----------движение------
    private void move(KeyCode arrow, KeyCode wasd, bool phone_arrow, int move_x, int move_y, string move)
    {
        if (Input.GetKeyDown(arrow) && стрелочки == 0 || Input.GetKeyDown(wasd) && WASD == 0 || phone_arrow)
        {
            if (LORD.moves_bat)
            {
                count_moves++;
                if (count_moves >= LORD.moves_bat_count && LORD.moves_bat)
                {
                    count_moves = 0;
                    peregenetate(new char[2] { 'B', 'b' });
                    pole_bat.ClearAllTiles();
                    pole_bat_dop.ClearAllTiles();
                    generate(LORD.BAT, "B", "b", new string[4] { "B", "P", "p", "W" }, false);
                }
            }
            pole_player.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), null);
            Y_player = (Y_player + move_y + LORD.LEN) % LORD.LEN;
            X_player = (X_player + move_x + LORD.LEN) % LORD.LEN;
            if (move == "left")
            { rotate = false; }
            else if (move == "right")
            { rotate = true; }
            if (rotate)
            {
                pole_player.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), player_r);
            }
            else
            {
                pole_player.SetTile(new Vector3Int(X_player - half_len, Y_player - half_len, 0), player_l);
            }
            render();
            left = false; right = false; down = false; up = false;
        }
    }
    //----------атака------
    private void attack(KeyCode arrow, KeyCode wasd, bool phone_arrow, int attack_x, int attack_y)
    {
        if ((Input.GetKeyDown(arrow) && стрелочки == 1) || (Input.GetKeyDown(wasd) && WASD == 1) || phone_arrow)
        {
            if ((Y_player + attack_y + LORD.LEN) % LORD.LEN == Y_wumpus
             && (X_player + attack_x + LORD.LEN) % LORD.LEN == X_wumpus)
            {
                sound_2.volume = LORD.слайдер * 0.2f;
                new_text.text = "Вы победили !";
            }
            else
            {
                TEXT.text = "Вы промазали";
            }
            GameOver();
        }
    }
    void peregenetate(char[] _unit)
    {
        for (int x = 0; x < MAP.Length; x++)
        {
            for (int y = 0; y < MAP.Length; y++)
            {
                MAP[x][y] = string.Concat(MAP[x][y].Split(_unit.ToArray()));
            }
        }
        peregenerated = true;
    }
    void del_text()
    {
        bat_text.text = "";
    }
}
