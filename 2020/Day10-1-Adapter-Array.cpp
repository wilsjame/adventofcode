#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);
    int k;
    vector<int> A = {0};
    while(cin >> k) A.push_back(k);
    sort(A.begin(), A.end());
    A.push_back(A.back() + 3);

    int one = 0, three = 0;
    for (int i = 0; i + 1 < A.size(); i++) {
        int d = A[i + 1] - A[i];
        one += d == 1;
        three += d == 3;
    }
    cout << one * three << endl;

    return 0;
}
