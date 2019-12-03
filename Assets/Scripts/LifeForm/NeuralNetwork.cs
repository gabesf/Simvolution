using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Neural
{
    class SimpleNeuralNet
    {
        
    }

    class Pulse
    {
        public double Value { get; set; }
    }

    class Dendrite
    {
        public Dendrite()
        {
            InputPulse = new Pulse();
        }

        public Pulse InputPulse { get; set; }

        public double SynapticWeight { get; set; }

        public bool Learnable { get; set; } = true;
    }

    class Neuron
    {
        public List<Dendrite> Dendrites { get; set; }

        public Pulse OutputPulse { get; set; }

        public Neuron()
        {
            Dendrites = new List<Dendrite>();
            OutputPulse = new Pulse();
        }

        public void Fire()
        {
            //Debug.Log("Inside a Neuron Value = " + OutputPulse.Value);
            OutputPulse.Value = Sum();
            //Debug.Log("Still Inside, after Sum = " + OutputPulse.Value);
            OutputPulse.Value = Activation(OutputPulse.Value);
        }

        public void UpdateWeights(double new_weights)
        {
            foreach (var terminal in Dendrites)
            {
                terminal.SynapticWeight = new_weights;
            }
        }

        private double Sum()
        {
            double computeValue = 0.0f;
            //Debug.Log($"This Neuron receives {Dendrites.Count} dendtrites");
            int i = 0;
            foreach (var d in Dendrites)
            {
                //Debug.Log($"Dendrite {i} value = {d.InputPulse.Value}");
                computeValue += d.InputPulse.Value * d.SynapticWeight;
                i++;
            }

            return computeValue;
        }

        private double Activation(double input)
        {
            return Mathf.Atan((float)input);
            //double threshold = 1;
            //return input <= threshold ? 0 : threshold;
        }
    }

    class NeuralLayer
    {
        public List<Neuron> Neurons { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public NeuralLayer(int count, double initialWeight, string name = "")
        {
            Neurons = new List<Neuron>();
            for (int i = 0; i < count; i++)
            {
                Neurons.Add(new Neuron());
            }

            Weight = initialWeight;

            Name = name;
        }

        public void Optimize(double learningRate, double delta)
        {
            Weight += learningRate * delta;
            foreach (var neuron in Neurons)
            {
                neuron.UpdateWeights(Weight);
            }
        }

        public void Forward()
        {
            int i = 0;
            foreach (var neuron in Neurons)
            {
                //Debug.Log("Processing Neuron #" + i);
                neuron.Fire();
                i++;
            }
        }

        public void Log()
        {
            //Debug.Log($"{Name}, Weight: {Weight}");
        }
    }

    class NetworkModel
    {
        public List<NeuralLayer> Layers { get; set; }

        public NetworkModel()
        {
            Layers = new List<NeuralLayer>();
        }

        public void AddLayer(NeuralLayer layer)
        {
            int dendriteCount = 1;

            if (Layers.Count > 0)
            {
                dendriteCount = Layers.Last().Neurons.Count;
            }

            foreach (var element in layer.Neurons)
            {
                for (int i = 0; i < dendriteCount; i++)
                {
                    element.Dendrites.Add(new Dendrite());
                }
            }
        }

        public void Build()
        {
            int i = 0;
            foreach (var layer in Layers)
            {
                if (i >= Layers.Count - 1)
                {
                    break;
                }

                var nextLayer = Layers[i + 1];
                CreateNetwork(layer, nextLayer);

                i++;
            }
        }

        public List<double> Process(NeuralData X)
        {
            //Get the input layers
            var inputLayer = Layers[0];
            List<double> outputs = new List<double>();

            //Loop through the record
            Debug.Log(X.Data.Length);
            for (int i = 0; i < X.Data.Length; i++)
            {
                //Set the input data into the first layer
                Debug.Log($"Will add values to {X.Data[i].Length} nodes at input layer");
                for (int j = 0; j < X.Data[i].Length; j++)
                {
                    Debug.Log($"Putting {X.Data[i][j]} at Neuron #{j}");
                    inputLayer.Neurons[j].OutputPulse.Value = X.Data[i][j];
                }

                //Fire all the neurons and collect the output
                ComputeOutput();
                
                //outputs.Add(Layers.Last().Neurons.First().OutputPulse.Value);
            }

            for (int j = 0; j < Layers.Last().Neurons.Count; j++)
            {
                outputs.Add(Layers.Last().Neurons[j].OutputPulse.Value);
                //Debug.Log($"Neuron #{j}");
            }

            return outputs;

        }

        public void Train(NeuralData X, NeuralData Y, int iterations, double learningRate = 0.1)
        {
            int epoch = 1;
            //Loop till the number of iterations
            while (iterations >= epoch)
            {
                //Get the input layers
                var inputLayer = Layers[0];
                List<double> outputs = new List<double>();

                //Loop through the record
                Debug.Log("The X.Data.Lenght is " + X.Data.Length);
                for (int i = 0; i < X.Data.Length; i++)
                {
                    //Set the input data into the first layer

                    
                    for (int j = 0; j < X.Data[i].Length; j++)
                    {
                        inputLayer.Neurons[j].OutputPulse.Value = X.Data[i][j];
                    }

                    //Fire all the neurons and collect the output
                    ComputeOutput();
                    outputs.Add(Layers.Last().Neurons.First().OutputPulse.Value);
                }

                //Debug.Log("Inside Train:");
                for(int i = 0; i < outputs.Count; i++)
                {
                    //Debug.Log(outputs[i]);
                }
                //Check the accuracy score against Y with the actual output
                double accuracySum = 0;
                int y_counter = 0;
                outputs.ForEach((x) => {
                    if (x == Y.Data[y_counter].First())
                    {
                        accuracySum++;
                    }

                    y_counter++;
                });

                //Optimize the synaptic weights
                OptimizeWeights(accuracySum / y_counter);
                //Debug.Log($"Epoch: {epoch}, Accuracy: {(accuracySum / y_counter) * 100} %");
                epoch++;
            }
        }

        public void Print()
        {
            //Debug.Log("Name   Neurons   Weight");
            for(int i = 0; i < Layers.Count; i++)
            {
                //Debug.Log(Layers[i].Name + " " + Layers[i].Neurons.Count + " " + Layers[i].Weight);
            }
            /*DataTable dt = new DataTable(); 
            dt.Columns.Add("Name");
            dt.Columns.Add("Neurons");
            dt.Columns.Add("Weight");

            foreach (var element in Layers)
            {
                DataRow row = dt.NewRow();
                row[0] = element.Name;
                row[1] = element.Neurons.Count;
                row[2] = element.Weight;

                dt.Rows.Add(row);
            }

            ConsoleTableBuilder builder = ConsoleTableBuilder.From(dt);
            builder.ExportAndWrite();*/
        }

        private void ComputeOutput()
        {
            bool first = true;
            int count = 0;
            foreach (var layer in Layers)
            {
                
                //Skip first layer as it is input
                if (first)
                {
                    first = false;
                    continue;
                }

                //Debug.Log("Processing Layer");
                layer.Forward();
            }
        }

        private void OptimizeWeights(double accuracy)
        {
            float lr = 0.1f;
            //Skip if the accuracy reached 100%
            if (accuracy == 1)
            {
                return;
            }

            if (accuracy > 1)
            {
                lr = -lr;
            }

            //Update the weights for all the layers
            foreach (var layer in Layers)
            {
                layer.Optimize(lr, 1);
            }
        }

        private void CreateNetwork(NeuralLayer connectingFrom, NeuralLayer connectingTo)
        {
            foreach (var from in connectingFrom.Neurons)
            {
                from.Dendrites = new List<Dendrite>();
                from.Dendrites.Add(new Dendrite());
            }

            foreach (var to in connectingTo.Neurons)
            {
                to.Dendrites = new List<Dendrite>();
                foreach (var from in connectingFrom.Neurons)
                {
                    to.Dendrites.Add(new Dendrite() { InputPulse = from.OutputPulse, SynapticWeight = connectingTo.Weight });
                }
            }
        }
    }

    class NeuralData
    {
        public double[][] Data { get; set; }

        int counter = 0;

        public NeuralData(int rows)
        {
            Data = new double[rows][];
        }

        public void Add(params double[] rec)
        {
            Data[counter] = rec;
            counter++;
        }
    }
}
