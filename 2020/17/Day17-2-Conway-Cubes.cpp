#include <iostream>
#include <string>
#include <vector>
using namespace std;

#define INPUTSIZE 8 // <- yo!
#define INF 100 + 5 
#define N INF - 5
#define print(x) cerr << #x << " " << x << endl;

int neighbors(vector<vector<vector<vector<char>>>> &A, int x, int y, int z, int w) {
    int cnt = 0;
    for (int dx : {-1, 0, 1}) {
        for (int dy : {-1, 0, 1}) {
            for (int dz : {-1, 0, 1}) {
                for (int dw : {-1, 0, 1}) {
                    if (((dx == 0 && dy == 0) && dz == 0) && dw == 0) continue;
                    if (A[x + dx][y + dy][z + dz][w + dw] == '#') cnt++;
                }
            }
        }
    }
    return cnt;
}

int main() {
    freopen("input.txt", "r", stdin);
    vector<vector<vector<vector<char>>>> xyzw (INF, vector<vector<vector<char>>> (INF, vector<vector<char>> (INF, vector<char> (INF, '.')))); 
    vector<vector<vector<vector<char>>>> xyzw1;

    // populate space
    for (int i = 50; i < 50 + INPUTSIZE; i++) 
        for (int j = 50; j < 50 + INPUTSIZE; j++) 
            cin >> xyzw[i][j][50][50];
    xyzw1 = xyzw;

    // simulation
    for (int cycle = 0; cycle < 6; cycle++) {
        for (int i = 1; i < N; i++) {
            for (int j = 1; j < N; j++) {
                for (int k = 1; k < N; k++) {
                    for (int q = 1; q < N; q++) {
                        char c = xyzw[i][j][k][q];
                        int n = neighbors(xyzw, i, j, k, q);
                        if (c == '#' && (n != 2 && n != 3)) 
                            xyzw1[i][j][k][q] = '.';
                        else if (c == '.' && n == 3) 
                            xyzw1[i][j][k][q] = '#';
                    }
                }
            }
        }
        xyzw = xyzw1;
    }

    // active cubes
    int ans = 0;
    for (int i = 1; i < N; i++) 
        for (int j = 1; j < N; j++) 
            for (int k = 1; k < N; k++) 
                for (int q = 1; q < N; q++)
                    ans += xyzw[i][j][k][q] == '#';
    print(ans);
            
    return 0;
}
