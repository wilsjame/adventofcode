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

    int x = 10, y = 1, Dx = 0, Dy = 0;
    for (auto s : A) {
        char a = s[0];
        s.erase(s.begin());
        int k = stoi(s);
        if (a == 'N') 
            y += k;
        else if (a == 'S') 
            y -= k;
        else if (a == 'E') 
            x += k;
        else if (a == 'W') 
            x -= k;
        else if (a == 'L') {
            int xp = x, yp = y;
            if (k / 90 == 1) 
                x = -yp, y = xp;
            else if (k / 90 == 2) 
                x = -x, y = -y;
            else 
                y = -xp, x = yp;
        }
        else if (a == 'R') {
            int xp = x, yp = y;
            if (k / 90 == 1) 
                y = -xp, x = yp;
            else if (k / 90 == 2) 
                x = -x, y = -y;
            else 
                x = -yp, y = xp;
        }
        else 
            Dx += k * x, Dy += k * y;
    }
    cout << abs(Dx) + abs(Dy) << endl;

    return 0;
}
