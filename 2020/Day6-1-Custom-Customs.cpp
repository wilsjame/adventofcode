#include <iostream>
#include <string>
#include <set>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);
    string S;
    set<int> cnt;

    int ans = 0;
    while (getline(cin, S)) {
        if (!S.empty()) {
            for (char c : S) cnt.insert(c);
            if (cin.peek() == EOF) ans += cnt.size();
        }
        else {
            ans += cnt.size();
            cnt.clear();
        }
    }
    cout << ans << endl;

    return 0;
}
