#include <iostream>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);        

    int x;
    int m[10000] = {};
    while (cin >> x) {
        m[x]++;
        if (m[2020 - x] > 0) {
            cout << x * (2020 - x) << endl;
            break;
        }
    }

    return 0;
}
