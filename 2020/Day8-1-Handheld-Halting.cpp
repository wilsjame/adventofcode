#include <iostream>
#include <string>
#include <vector>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);
    string instruction;
    vector<string> A; 
    while (getline(cin, instruction)) A.push_back(instruction);

    vector<bool> visited(10000, false);
    for (int i = 0, sum = 0; !visited[i];) {
        string op = A[i].substr(0, A[i].find(' '));
        int arg = stoi(A[i].substr(A[i].find(' ') + 1, A[i].size()));

        visited[i] = true;
        if (op == "acc") 
            sum += arg;
        else if (op == "jmp") 
            i += arg - 1;
        if (visited[++i]) 
            cout << sum << endl;
    }

    return 0;
}
