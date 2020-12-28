#include <iostream>
#include <string>
#include <vector>
using namespace std;

#define MAX 50
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
    int tile[MAX][MAX][MAX] = {};
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

    int ans = 0;
    for (int x = 1; x < MAX; x++) 
        for (int y = 1; y < MAX; y++) 
            for (int z = 1; z < MAX; z++) 
                if (tile[x][y][z] % 2) 
                    ans++;
    print(ans);

    return 0;
}
