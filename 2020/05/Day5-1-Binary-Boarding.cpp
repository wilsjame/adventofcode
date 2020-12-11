#include <iostream>
#include <string>
#include <algorithm>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);
    int ans = -1;
    string S;

    while (getline(cin, S)) {
        int a = 0, b = 127, r, c;
        for (int i = 0; i < 7; i++) {
            int k = (a + b) / 2;
            if (i == 6) {
                S[i] == 'F' ? r = a : r = b;
                break;
            }
            if (S[i] == 'F') b = k;
            else a = k + 1;
            r = k;
        }
        a = 0, b = 7;
        for (int i = 7; i < 10; i++) {
            int k = (a + b) / 2;
            if (i == 9) {
                S[i] == 'L' ? c = a : c = b;
                break;
            }
            if (S[i] == 'L') b = k;
            else a = k + 1;
            c = k;
        }
        ans = max(ans, r * 8 + c);
    }
    cout << ans << endl;

    return 0;
}
