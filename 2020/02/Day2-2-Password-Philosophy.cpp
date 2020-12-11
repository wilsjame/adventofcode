#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);
    string s;
    vector<string> A;
    while (getline(cin, s)) A.push_back(s);

    int ans = 0;
    for (string s : A) {
        // a-b c: remaining_s
        int a, b;
        char c;
        size_t p = 0;

        string d = "-";
        p = s.find(d);
        a = stoi(s.substr(0, s.find(d)));
        s.erase(0, p + d.length());

        d = " ";
        p = s.find(d);
        b = stoi(s.substr(0, p)); 
        s.erase(0, p + d.length());

        c = s[0];
        s.erase(0, 3);
        ans += ((s[a - 1] == c) + (s[b - 1] == c) == 1);
    }
    cout << ans << endl;

    return 0;
}
