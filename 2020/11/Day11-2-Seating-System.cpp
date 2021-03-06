#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);
    string S; 
    vector<char> row;
    vector<vector<char>> A, B;
    while (getline(cin, S )) {
        row = {'X'};
        for (char c : S) row.push_back(c);
        row.push_back('X');
        A.push_back(row);
    }
    fill(row.begin(), row.end(), 'X');
    A.insert(A.begin(), row);
    A.push_back(row);
    B = A;

    int R = A[0].size() - 1, C = A.size() - 1, ans = 0;
    while (1) {
        A = B;
        for (int i = 1; i < C; i++) {
            for (int t, j = 1; j < R; j++) {
                t = 0;
                for (int k = 1; A[i + k][j - k] != 'X' && A[i + k][j - k] != 'L'; k++) { 
                    if (A[i + k][j - k] == '#') {
                        t++; 
                        break;
                    }
                }
                for (int k = 1; A[i + k][j] != 'X' && A[i + k][j] != 'L'; k++) {
                    if (A[i + k][j] == '#') {
                        t++;
                        break;
                    }
                }
                for (int k = 1; A[i + k][j + k] != 'X' && A[i + k][j + k] != 'L'; k++) {
                    if (A[i + k][j + k] == '#') {
                        t++; 
                        break;
                    }
                }
                for (int k = 1; A[i][j + k] != 'X' && A[i][j + k] != 'L'; k++) {
                    if (A[i][j + k] == '#') {
                        t++; 
                        break;
                    }
                }
                for (int k = 1; A[i - k][j + k] != 'X'&& A[i - k][j + k] != 'L'; k++) {
                    if (A[i - k][j + k] == '#') {
                        t++; 
                        break;
                    }
                }
                for (int k = 1; A[i - k][j] != 'X' && A[i - k][j] != 'L'; k++) {
                    if (A[i - k][j] == '#') {
                        t++; 
                        break;
                    }
                }
                for (int k = 1; A[i - k][j - k] != 'X' && A[i - k][j - k] != 'L'; k++) {
                    if (A[i - k][j - k] == '#') {
                        t++; 
                        break;
                    }
                }
                for (int k = 1; A[i][j - k] != 'X' && A[i][j - k] != 'L'; k++) {
                    if (A[i][j - k] == '#') {
                        t++; 
                        break;
                    }
                }
                if (A[i][j] == 'L' && t == 0) 
                    B[i][j] = '#';
                else if (A[i][j] == '#' && t >= 5) 
                    B[i][j] = 'L';
            }
        }
        if (A == B) break;
    }
    for (auto row : B) 
        for (auto a : row) 
            ans += a == '#';
    cout << ans << endl;

    return 0;
}
