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
        int a, b, cnt;
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
        cnt = count(s.begin(), s.end(), c);
        ans += (cnt >= a && cnt <= b);
    }
    cout << ans << endl;

    return 0;
}
