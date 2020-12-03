#include <iostream>
#include <string>
#include <vector>
#include <utility>
using namespace std;

#define ll long long

int main() {
    freopen("input.txt", "r", stdin);
    string S;
    vector<vector<char>> A;
    vector<pair<int, int>> slope = {{1, 1}, {3, 1}, {5, 1}, {7, 1}, {1, 2}};
    while (getline(cin, S)) {
        vector<char> row;
        for (char c : S) {  
            row.push_back(c);
        }
        A.push_back(row);
    }

    ll ans = 1;
    for (auto pr : slope) {
        int cnt = 0;
        for (int i = 0, j = 0;; i += pr.second, j += pr.first) {
            if (i >= A.size()) {
                break;
            }
            else if (j >= A[i].size()) {
                j = j - A[i].size();
            }
            cnt += A[i][j] == '#';
        }
        ans *= cnt;
    }
    cout << ans << endl;

    return 0;
}
