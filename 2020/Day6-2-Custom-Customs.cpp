#include <iostream>
#include <string>
#include <unordered_map>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);
    string S;
    unordered_map<char, int> m;

    // count keys with value equal to the number of people in group
    int p = 0, ans = 0;
    while (getline(cin, S)) {
        if (!S.empty()) {
            p++;
            for (char c : S) m[c]++;
            if (cin.peek() == EOF) 
                for (auto pr : m) ans += p == pr.second;
        }
        else {
            for (auto pr : m) ans += p == pr.second;
            m.clear();
            p = 0;
        }
    }
    cout << ans << endl;

    return 0;
}
