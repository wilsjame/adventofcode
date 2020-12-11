#include <iostream>
#include <vector>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);        

    int x;
    vector<int> A;
    while (cin >> x) {
        A.push_back(x);
    }

    for (int i = 0; i < (int)A.size(); i++) {
        for (int j = i + 1; j < (int)A.size(); j++) {
            for (int k = j + 1; k < (int)A.size(); k++) {
                if (A[i] + A[j] + A[k] == 2020) {
                    cout << A[i] * A[j] * A[k];
                    return 0;
                }
            }
        }
    }

    return 0;
}
