#include <iostream>
#include <string>
#include <vector>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);
    string S;
    vector<vector<char>> A;
    while (getline(cin, S)) {
        vector<char> row;
        for (char c : S) {  
            row.push_back(c);
        }
        A.push_back(row);
    }

    int ans = 0;
    for (int i = 0, j = 0;; i++, j += 3) {
        if (i >= A.size()) {
            break;
        }
        else if (j >= A[i].size()) {
            j = j - A[i].size();
        }
        ans += A[i][j] == '#';
    }
    cout << ans << endl;

    return 0;
}
