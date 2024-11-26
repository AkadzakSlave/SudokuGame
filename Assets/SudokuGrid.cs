using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuGrid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float every_sq_offset = 0.0f;
    public GameObject sudoku_grid;
    public Vector2 start_pos = new Vector2(0.0f, 0.0f);
    public float squar_scale = 1.0f;
    
    public List<GameObject> grid_sqaures_ = new List<GameObject>();
    private int selected_grid_data = -1;
    void Start()
    {
        if (sudoku_grid.GetComponent<GridSquare>() == null)
            Debug.LogError("sudoku_grid не отображен");
        CreatSudGrid();
        SetGridNumber(GameSettings.Instance.GetGameMode());
    }
    private void CreatSudGrid()
    {
        Squares();
        Positions();
    }
    private void Squares()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                grid_sqaures_.Add(Instantiate(sudoku_grid) as GameObject);
                grid_sqaures_[grid_sqaures_.Count - 1].transform.parent = this.transform;
                grid_sqaures_[grid_sqaures_.Count - 1].transform.localScale =
                    new Vector3(squar_scale, squar_scale, squar_scale);
            }
        }
    }
    private void Positions()
    {
        var square_rect = grid_sqaures_[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        offset.x = square_rect.rect.width * square_rect.transform.localScale.x + every_sq_offset;
        offset.y = square_rect.rect.height * square_rect.transform.localScale.y + every_sq_offset;
        
        int column_num = 0;
        int row_num = 0;
        foreach (GameObject square in grid_sqaures_)
        {
            if (column_num + 1 > columns)
            {
                column_num = 0;
                row_num++;
            }
            var pos_x_offset = offset.x * column_num;
            var pos_y_offset = offset.y * row_num;
            square.GetComponent<RectTransform>().anchoredPosition =
                new Vector3(start_pos.x + pos_x_offset, start_pos.y - pos_y_offset);
            column_num++;
        }
    }
    private void SetGridNumber(string level)
    {
        selected_grid_data = Random.Range(0, SudokuData.Instance.sudoku_game[level].Count);
        var data = SudokuData.Instance.sudoku_game[level][selected_grid_data];
        
        SetGridSquareData(data);
        
        // foreach (var square in grid_sqaures_)
        // {
        //     square.GetComponent<GridSquare>().SetNumber(Random.Range(0,10));
        // }
    }
    private void SetGridSquareData(SudokuData.SudokuBoardData data)
    {
        for (int index = 0; index < grid_sqaures_.Count; index++)
        {
            grid_sqaures_[index].GetComponent<GridSquare>().SetNumber(data.Unsolved[index]);
        }
    }
}


