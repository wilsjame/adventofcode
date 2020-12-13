#include <iostream>
#include <string>
#include <vector>
#include <cmath>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);
    string S;
    vector<string> A;
    while (getline(cin, S)) A.push_back(S);

    vector<int> D = {0, 0, 0, 0}; // {E, S, W, N}
    int idx = 0;
    for (auto s : A) {
        char a = s[0];
        s.erase(s.begin());
        int k = stoi(s);
        if (a == 'N') 
            D[3] += k;
        else if (a == 'S') 
            D[1] += k;
        else if (a == 'E') 
            D[0] += k;
        else if (a == 'W') 
            D[2] += k;
        else if (a == 'L') 
            idx - k / 90 < 0 ? idx = 4 + (idx - k / 90)  : idx -= k / 90;
        else if (a == 'R') {
            idx = (idx + (k / 90)) % 4;
        else
            D[idx] += k;
    }
    cout << abs(D[0] - D[2]) + abs(D[1] - D[3]) << endl;

    return 0;
}
