using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using SteamAPI.Logics;
using SteamAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;

namespace SteamAPI.Modules
{
    public class Favorite
    {
        public async void FavoritePlus(string Name, string BattleTag)
        {
            if (Name == "" || BattleTag == "")
            {
                await Commons.ShowMessageAsync("오류", "먼저 조회를 해야합니다.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Commons.myConnString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                    var query = @"INSERT INTO overwatch
                                         (Name,
                                         BattleTag)
                                         VALUES
                                         (@Name ,
                                         @BattleTag );";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@BattleTag", BattleTag);

                    int affectedRows = await cmd.ExecuteNonQueryAsync(); // INSERT 쿼리 실행

                    await Commons.ShowMessageAsync("저장", "DB저장 성공!!");
                }   
            }
            catch (Exception ex)
            {
                await Commons.ShowMessageAsync("오류", $"DB 저장 오류! {ex.Message}");
            }
        }

        public async void FavoriteView(System.Windows.Controls.DataGrid result)
        {
            try
            {
                
                using (MySqlConnection conn = new MySqlConnection(Commons.myConnString))
                {
                    await conn.OpenAsync();
                    var query = @"SELECT name,
                                         battletag
                                         FROM overwatch";
                    var command = new MySqlCommand(query, conn);
                    var adapter = new MySqlDataAdapter(command);
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    result.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                await Commons.ShowMessageAsync("오류", $"DB 저장 오류! {ex.Message}");
            }
        }

        public async void FavoriteDelete(string BattleTag)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Commons.myConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = @"DELETE FROM overwatch
                                         WHERE BattleTag = @BattleTag";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BattleTag", BattleTag);

                    int result = await cmd.ExecuteNonQueryAsync();
                    if (result == 1)
                    {
                        await Commons.ShowMessageAsync("삭제", "삭제되었습니다.");
                    }
                }
            }
            catch (Exception ex)
            {
                await Commons.ShowMessageAsync("오류", $"삭제오류! {ex.Message}");
            }
        }

    }
}
