# Behavior Tree Implementation in C#

This project provides a C# implementation of a Behavior Tree (BT), a popular AI technique used in game development and robotics for autonomous agents.

## Description

Behavior Trees are hierarchical structures of nodes that control the decision-making process of an AI entity. Each node in the tree represents a specific behavior or a control flow construct. The tree is traversed from the root, and nodes are executed based on their type and the state of the game world or environment. This implementation provides a flexible framework for creating various types of BT nodes and combining them to design sophisticated AI behaviors.

## Core Components

The project is structured around the following core components:

* **`BTNode` (BT-Implementation/BTNode.cs)**: An abstract base class for all nodes in the Behavior Tree. Each node has a `Name` and an `Execute()` method that returns a `BTResult`.
* **`BTResult` (BT-Implementation/BTResult.cs)**: An enumeration representing the possible outcomes of a node's execution:
    * `Success`: The node completed its task successfully.
    * `Failure`: The node failed to complete its task.
    * `Running`: The node is still executing and needs more time.
* **`Blackboard` (BT-Implementation/BlackBoard.cs)**: A shared data repository that nodes can use to store and retrieve information. This allows different parts of the tree to communicate and share state.
* **`BTRoot` (BT-Implementation/BTRoot.cs)**: An abstract class representing the root of the Behavior Tree. It holds a reference to the `Blackboard` and defines methods to `ConstructBT()` and `ExecuteBT()`.
* **`IAbstractBlackboardFactory` (BT-Implementation/IAbstractBlackboardFactory.cs)**: An interface for creating `Blackboard` instances.
* **`NullBTNode` (BT-Implementation/NullBTNode.cs)**: A simple node that always returns `Failure`. It can be used as a placeholder or a default child.

## Node Types

The implementation includes several types of nodes, categorized as Control Nodes, Decorator Nodes, and Leaf Nodes.

### Control Nodes (`BT-Implementation/Control/`)

Control nodes manage the execution flow of their child nodes.

* **`ControlNode` (BT-Implementation/Control/ControlNode.cs)**: An abstract base class for control nodes, which can have multiple children.
* **`SequenceNode` (BT-Implementation/Control/SequenceNode.cs)**: Executes its children sequentially until one of them returns `Failure` or `Running`, or all of them return `Success`. If a child fails, the sequence node fails. If all children succeed, the sequence node succeeds.
* **`SelectorNode` (BT-Implementation/Control/SelectorNode.cs)**: Executes its children sequentially until one of them returns `Success` or `Running`. If a child succeeds, the selector node succeeds. If all children fail, the selector node fails.
* **`ParallelNode` (BT-Implementation/Control/ParallelNode.cs)**: Executes all its children concurrently. It succeeds or fails based on a specified success threshold (number of children that need to succeed).

### Decorator Nodes (`BT-Implementation/Decorator/`)

Decorator nodes modify the behavior of a single child node.

* **`DecoratorNode` (BT-Implementation/Decorator/DecoratorNode.cs)**: An abstract base class for decorator nodes, which have a single child.
* **`InverterNode` (BT-Implementation/Decorator/InverterNode.cs)**: Inverts the result of its child node. `Success` becomes `Failure`, and `Failure` becomes `Success`. `Running` remains `Running`.
* **`RetryNode` (BT-Implementation/Decorator/RetryNode.cs)**: Re-executes its child node a specified number of times (or infinitely if set to -1) if the child returns `Failure`. It succeeds if the child eventually succeeds within the retry limit.
* **`RepeatNode` (BT-Implementation/Decorator/RepeatNode.cs)**: Re-executes its child node a specified number of times as long as the child returns `Success`. It fails if the child fails at any point.
* **`DelayNode` (BT-Implementation/Decorator/DelayNode.cs)**: Introduces a delay before executing its child node or after the child completes, based on its internal timer logic.

### Leaf Nodes (`BT-Implementation/Leaf/`)

Leaf nodes represent the actual actions or conditions at the lowest level of the tree.

* **`ActionNode` (BT-Implementation/Leaf/ActionNode.cs)**: An abstract base class for nodes that perform actions. These nodes typically interact with the game world or agent's state via the `Blackboard`.
* **`ConditionNode` (BT-Implementation/Leaf/ConditionNode.cs)**: Evaluates a predicate function (a condition) using the `Blackboard`. It returns `Success` if the condition is true and `Failure` otherwise.

## How to Use (General Idea)

1.  **Define Actions and Conditions:**
    * Create concrete classes inheriting from `ActionNode` to implement specific actions your AI should perform (e.g., `MoveToTargetNode`, `AttackEnemyNode`).
    * Use `ConditionNode` by providing a predicate function to check world states or agent properties (e.g., `IsEnemyNearbyCondition`, `HasLowHealthCondition`).
2.  **Construct the Behavior Tree:**
    * Instantiate the desired control, decorator, and leaf nodes.
    * Assemble them into a tree structure by adding children to control nodes and setting the child for decorator nodes.
    * Create a class inheriting from `BTRoot` and implement `ConstructBT()` to build your specific tree.
3.  **Execute the Tree:**
    * Create an instance of your `BTRoot` derivative, providing it with a `Blackboard` instance (potentially created via an `IAbstractBlackboardFactory`).
    * Call the `ConstructBT()` method on your root node.
    * In your game loop or AI update cycle, call the `ExecuteBT()` method on the root node. This will traverse the tree and execute the appropriate behaviors.
4.  **Use the Blackboard:**
    * Store relevant game state information or agent parameters in the `Blackboard`.
    * Access and modify this data within your `ActionNode` and `ConditionNode` implementations to make decisions and update state.




For unity compiled binaries exists  check BT-Implementation bin directory ( use 2.0 )
Can be used any application.
Possible improvments 
* Code generation based on node based UI interface can be very good.
