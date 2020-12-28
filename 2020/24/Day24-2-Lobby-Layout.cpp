#include <iostream>
#include <string>
#include <vector>
using namespace std;

#define MAX 200 
#define print(x) cerr << #x << " is " << x << endl;

int main() {
    freopen("input.txt", "r", stdin);
    string S;
    vector<string> A;
    vector<vector<string>> D;
    while (getline(cin, S)) A.push_back(S);
    for (auto row : A) {
        vector<string> a;
        for (int i = 0; i < row.length(); i++) {
            char c1 = row[i];
            string d = string(1, c1);
            if (c1 == 's' || c1 == 'n') {
                char c2 = row[++i];
                d += c2;
            }
            a.push_back(d);
        }
        D.push_back(a);
    }

    // cube coordinates
    vector<vector<vector<int>>> tile_new, tile(MAX, vector<vector<int>> (MAX, vector<int> (MAX, 0)));
    int origin = MAX / 2;
    for (auto row : D) {
        int x = 0, y = 0, z = 0;
        for (auto dir : row) {
            if (dir == "w") 
                --x, ++y;
            else if (dir == "nw") 
                ++y, ++z;
            else if (dir == "ne") 
                ++z, ++x;
            else if (dir == "e") 
                ++x, --y;
            else if (dir == "se") 
                --y, --z;
            else if (dir == "sw") 
                --z, --x;
        }
        tile[origin + x][origin + y][origin + z]++;
    }
    tile_new = tile;

    for (int day = 1; day <= 100; day++) {
        for (int x = 1; x < MAX - 1; x++) { 
            for (int y = 1; y < MAX - 1; y++) {
                for (int z = 1; z < MAX - 1; z++) { 
                    int adj_black = 0;
                    adj_black += tile[x - 1][y + 1][z] % 2;
                    adj_black += tile[x][y + 1][z + 1] % 2;
                    adj_black += tile[x + 1][y][z + 1] % 2;
                    adj_black += tile[x + 1][y - 1][z] % 2;
                    adj_black += tile[x][y - 1][z - 1] % 2;
                    adj_black += tile[x - 1][y][z - 1] % 2;
                    if (tile[x][y][z] % 2 && (adj_black == 0 || adj_black > 2)) 
                        tile_new[x][y][z]++;
                    else if (tile[x][y][z] % 2 == 0 && adj_black == 2) 
                        tile_new[x][y][z]++;
                }
            }
        }
        tile = tile_new;
    }

    int ans = 0;
    for (int i = 0; i < MAX; i++) 
        for (int j = 0; j < MAX; j++) 
            for (int k = 0; k < MAX; k++) 
                if (tile[i][j][k] % 2) 
                    ans++;
    print(ans);

    return 0;
}
